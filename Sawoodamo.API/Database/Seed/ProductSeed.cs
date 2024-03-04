using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                IsDeleted = false,
                Order = 1,
                Slug = "iphone-pro-max",
                IsActive = true,
                Name = "Iphone 15 Pro Max",
                FullDescription = "Iphone made by apple",
                ShortDescription = "Apple iphone",
                OriginalPrice = 5999m,
                CurrentPrice = 5999m,
                IsPinned = true,
            },
            new Product
            {
                Id = "6529374c-2166-4727-95ac-0c623aa21642",
                IsDeleted = false,
                Order = 2,
                Slug = "fridge",
                IsActive = true,
                Name = "Smart fridge",
                FullDescription = "Ultra super smart fridge made by google that makes food teleport",
                ShortDescription = "Bridge by google",
                OriginalPrice = 1200m,
                CurrentPrice = 1200m,
                IsPinned = true,
            }
        );
    }
}
