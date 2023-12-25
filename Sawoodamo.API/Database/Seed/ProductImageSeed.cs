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
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 2,
                ProductId = 1,
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 3,
                ProductId = 2,
                Order = 1,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = 4,
                ProductId = 2,
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/bde47de0-7671-4522-8edd-7140571543ee",
                IsActive = true,
                IsDeleted = false,
            }
        );
    }
}
