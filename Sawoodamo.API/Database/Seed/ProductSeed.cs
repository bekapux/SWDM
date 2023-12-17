using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public class ProductSeed : IEntityTypeConfiguration<Product>
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
                DateCreated = DateTime.Now,
                IsActive = true,
                Name = "Iphone 15 Pro Max",
                FullDescription = "Iphone made by apple",
                ShortDescription = "Apple iphone",
                CreatedBy = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f"
            },
            new Product
            {
                Id = 2,
                IsDeleted = false,
                Order = 2,
                Slug = "fridge",
                DateCreated = DateTime.Now,
                IsActive = true,
                Name = "Smart fridge",
                FullDescription = "Ultra super smart fridge made by google that makes food teleport",
                ShortDescription = "Bridge by google",
                CreatedBy = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f"
            }
        );
    }
}
