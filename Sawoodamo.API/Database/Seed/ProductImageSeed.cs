﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductImageSeed : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasData(
            new ProductImage
            {
                Id = 1,
                ProductId = 1,
                Order = 1,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/7bc54b67-6955-4ce1-aa3a-c8d975480b46",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 2,
                ProductId = 1,
                Order = 2,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 3,
                ProductId = 1,
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/f13eda0f-807d-4fc2-bea1-e4ab04a1a2e6",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 4,
                ProductId = 2,
                Order = 1,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,
            },
            new ProductImage
            {
                Id = 5,
                ProductId = 2,
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,
            }
        );
    }
}
