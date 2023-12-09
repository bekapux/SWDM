global using Carter;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Sawoodamo.API;
global using Sawoodamo.API.Database;
global using Sawoodamo.API.Database.Entities;
global using Sawoodamo.API.Database.Entities.Utilities;
global using Sawoodamo.API.Utilities;
global using Sawoodamo.API.Utilities.Extensions;
global using Sawoodamo.API.Utilities.Models;
global using FluentValidation;
global using Sawoodamo.API.Utilities.Exceptions;
global using Microsoft.AspNetCore.Diagnostics;
global using Sawoodamo.API.Utilities.Validation;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDoc();

        var thisAssembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(thisAssembly);
        services.AddMediatR(o => o.RegisterServicesFromAssemblies(thisAssembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddDbContext<SawoodamoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(SawoodamoDbContext).Assembly.FullName)));

        services.AddHttpContextAccessor();

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<SawoodamoDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(configuration);

        return services;
    }

    public static void AddSwaggerDoc(this IServiceCollection services)
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

    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
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
        });
    }
}