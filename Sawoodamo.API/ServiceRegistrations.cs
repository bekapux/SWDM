global using Amazon.S3;
global using Amazon.S3.Model;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Memory;
global using Sawoodamo.API;
global using Sawoodamo.API.Database;
global using Sawoodamo.API.Database.Entities;
global using Sawoodamo.API.Database.Entities.Utilities;
global using Sawoodamo.API.Features.Auth;
global using Sawoodamo.API.Features.Categories;
global using Sawoodamo.API.Features.ProductImages;
global using Sawoodamo.API.Features.Products;
global using Sawoodamo.API.Features.ProductSpecs;
global using Sawoodamo.API.Services;
global using Sawoodamo.API.Services.Abstractions;
global using Sawoodamo.API.Utilities;
global using Sawoodamo.API.Utilities.Exceptions;
global using Sawoodamo.API.Utilities.Extensions;
global using Sawoodamo.API.Utilities.Models;
global using Sawoodamo.API.Utilities.Validation;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using Sawoodamo.API.Features.Cart;
using Amazon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Sawoodamo.API;

public static class ServiceRegistrations
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication(configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerDoc();
        services.AddHttpContextAccessor();

        services.ConfigureDatabase(configuration);

        services.ConfigureMediatr();
        services.ConfigureAWSS3(configuration);
        services.ConfigureCors();
        services.AddMemoryCache();

        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }

    private static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors((o) => o.AddPolicy("debug", policy =>
        {
            policy.WithOrigins("http://localhost", "http://localhost:4200", "http://localhost:4000"  ) // Replace with the actual origins
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));
    }

    private static void ConfigureMediatr(this IServiceCollection services)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssemblyByOrder(thisAssembly);

        services.AddMediatR(o => o.RegisterServicesFromAssemblies(thisAssembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<SawoodamoDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<SawoodamoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(SawoodamoDbContext).Assembly.FullName));


            // Types that you want to Audit
            //HashSet<Type> auditTypes = [typeof(Product), typeof(Category)];

            // properties of types that you want to ignore
            //Dictionary<Type, HashSet<string>> ignoreEntityProperties = new()
            //{
            //    { typeof(Category), [nameof(Category.Order), nameof(Category.Name)] }
            //};

            //options.AddInterceptors(new AuditTrailInterceptor(auditTypes, [] /*ignoreEntityProperties*/));
        });
    }

    private static void ConfigureAWSS3(this IServiceCollection services, IConfiguration configuration)
    {
        var accessKey = configuration.GetSection("S3:AccessKey").Value;
        var roleSecret = configuration.GetSection("S3:Secret").Value;

        var awsOptions = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.EUCentral1,
        };
        services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(accessKey, roleSecret, awsOptions));
    }

    private static void AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Sawoodamo Api",
            });
            c.CustomSchemaIds(x => x.FullName);
        });
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
            };

            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey("authToken"))
                    {
                        context.Token = context.Request.Cookies["authToken"];
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }

    private static void AddValidatorsFromAssemblyByOrder(this IServiceCollection services, Assembly assembly)
    {
        var validators = AssemblyScanner.FindValidatorsInAssembly(assembly, false)
            .Select(v => new
            {
                Validator = v,
                Order = v.ValidatorType.GetCustomAttribute<ValidationOrderAttribute>()?.Order ?? int.MaxValue
            })
            .OrderBy(v => v.Order);

        foreach (var validator in validators)
        {
            services.Add(new ServiceDescriptor(validator.Validator.InterfaceType, validator.Validator.ValidatorType, ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(serviceType: validator.Validator.ValidatorType, implementationType: validator.Validator.ValidatorType, lifetime: ServiceLifetime.Scoped));
        };
    }
}