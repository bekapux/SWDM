using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 2,
                ProductId = 1,
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 3,
                ProductId = 2,
                Order = 1,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 4,
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
