using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductImageSeed : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasData(
            new ProductImage
            {
                Id = "5a6c6798-ace1-43ae-b196-834d1400f0bf",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                Order = 1,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/7bc54b67-6955-4ce1-aa3a-c8d975480b46",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = "1c5dce65-0777-4dca-8139-5223686a7766",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                Order = 2,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = "5edb9ff6-7928-4b03-86fd-c72b79e89089",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/f13eda0f-807d-4fc2-bea1-e4ab04a1a2e6",
                IsActive = true,
                IsDeleted = false,

            },
            new ProductImage
            {
                Id = "a21e84c2-6870-4da9-8c37-6c10ec8f0daa",
                ProductId = "6529374c-2166-4727-95ac-0c623aa21642",
                Order = 1,
                IsMainImage = true,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,
            },
            new ProductImage
            {
                Id = "1f6c076a-e293-4845-a5d4-c4d6f39bc4d1",
                ProductId = "6529374c-2166-4727-95ac-0c623aa21642",
                Order = 2,
                IsMainImage = false,
                Url = "https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c",
                IsActive = true,
                IsDeleted = false,
            }
        );
    }
}
