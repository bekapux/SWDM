using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = 1,
                IsDeleted = false,
                Order = 1,
                Slug = "iphone-pro-max",
                IsActive = true,
                Name = "Iphone 15 Pro Max",
                FullDescription = "Iphone made by apple",
                ShortDescription = "Apple iphone",
                Price = 5999m,
                IsPinned = true,
                Discount = 0,
            },
            new Product
            {
                Id = 2,
                IsDeleted = false,
                Order = 2,
                Slug = "fridge",
                IsActive = true,
                Name = "Smart fridge",
                FullDescription = "Ultra super smart fridge made by google that makes food teleport",
                ShortDescription = "Bridge by google",
                Price = 1200m,
                IsPinned = true,
                Discount = 10,
            }
        );
    }
}
