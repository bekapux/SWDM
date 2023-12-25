﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sawoodamo.API.Database;

#nullable disable

namespace Sawoodamo.API.Database.Migrations
{
    [DbContext(typeof(SawoodamoDbContext))]
    partial class SawoodamoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("Tokens", (string)null);
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.AuditTrail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EntityId")
                        .HasMaxLength(68)
                        .HasColumnType("nvarchar(68)");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ModifiesFieldsJoined")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasMaxLength(68)
                        .HasColumnType("nvarchar(68)");

                    b.HasKey("Id");

                    b.HasIndex("Action");

                    b.HasIndex("EntityType");

                    b.ToTable("AuditTrails");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Electronics",
                            Order = 1,
                            Slug = "electronics"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Kitchen",
                            Order = 2,
                            Slug = "kitchen"
                        });
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullDescription = "Iphone made by apple",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Iphone 15 Pro Max",
                            Order = 1,
                            ShortDescription = "Apple iphone",
                            Slug = "iphone-pro-max"
                        },
                        new
                        {
                            Id = 2,
                            FullDescription = "Ultra super smart fridge made by google that makes food teleport",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Smart fridge",
                            Order = 2,
                            ShortDescription = "Bridge by google",
                            Slug = "fridge"
                        });
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            IsDeleted = false
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsMainImage")
                        .HasColumnType("bit");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            IsDeleted = false,
                            IsMainImage = true,
                            Order = 1,
                            ProductId = 1,
                            Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            IsDeleted = false,
                            IsMainImage = false,
                            Order = 2,
                            ProductId = 1,
                            Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            IsDeleted = false,
                            IsMainImage = true,
                            Order = 1,
                            ProductId = 2,
                            Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            IsDeleted = false,
                            IsMainImage = false,
                            Order = 2,
                            ProductId = 2,
                            Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee"
                        });
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductSpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SpecName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SpecValue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSpec");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            ProductId = 1,
                            SpecName = "Weight",
                            SpecValue = "221g"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            ProductId = 1,
                            SpecName = "Display Size",
                            SpecValue = "6.7 inches"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            ProductId = 1,
                            SpecName = "OS",
                            SpecValue = "iOS 17"
                        });
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateRegistered")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a0df669f-7035-4b6c-82a0-5c726ae040f7",
                            DateRegistered = new DateTime(2023, 12, 25, 15, 55, 51, 703, DateTimeKind.Utc).AddTicks(7090),
                            Email = "beka.pukhashvili@gmail.com",
                            EmailConfirmed = true,
                            Firstname = "Beka",
                            IsActive = true,
                            IsAdmin = true,
                            IsDeleted = false,
                            Lastname = "Pukhashvili",
                            LockoutEnabled = true,
                            NormalizedEmail = "BEKA.PUKHASHVILI@GMAIL.COM",
                            NormalizedUserName = "BEKA.PUKHASHVILI",
                            PasswordHash = "AQAAAAIAAYagAAAAEO3jGAdV4e0Em3pcVP1akY73K6MP+8D7RxxTzRShjX5w87X7vVrWc8QqTnlqlgqP5A==",
                            PhoneNumber = "551345679",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "eb3f3232-f4c3-400c-b868-15bbcb5589dd",
                            TwoFactorEnabled = false,
                            UserName = "beka.pukhashvili"
                        },
                        new
                        {
                            Id = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f1342cc2-4490-40f1-8028-a69b31361305",
                            DateRegistered = new DateTime(2023, 12, 25, 15, 55, 51, 747, DateTimeKind.Utc).AddTicks(9227),
                            Email = "string",
                            EmailConfirmed = true,
                            Firstname = "string",
                            IsActive = true,
                            IsAdmin = true,
                            IsDeleted = false,
                            Lastname = "string",
                            LockoutEnabled = true,
                            NormalizedEmail = "STRING",
                            NormalizedUserName = "STRING",
                            PasswordHash = "AQAAAAIAAYagAAAAEOD7Dl95t9xbQ53Xbg3ufxd123gm8j1OZbuNgFG6qsDSA9Qh+PnLF1vOrNgkvufEZQ==",
                            PhoneNumber = "551345679",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0a6d84ef-3293-4c15-8371-3932aff02a40",
                            TwoFactorEnabled = false,
                            UserName = "string"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.CartItem", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sawoodamo.API.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductCategory", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sawoodamo.API.Database.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductImage", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.ProductSpec", b =>
                {
                    b.HasOne("Sawoodamo.API.Database.Entities.Product", null)
                        .WithMany("ProductSpecs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Sawoodamo.API.Database.Entities.Product", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductSpecs");
                });
#pragma warning restore 612, 618
        }
    }
}
