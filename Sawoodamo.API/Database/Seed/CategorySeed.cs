using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = 1,
                DateCreated = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                Name = "Electronics",
                Slug = "electronics",
                Order = 1,
                CreatedBy = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f"
            },
            new Category
            {
                Id = 2,
                DateCreated = DateTime.UtcNow.AddDays(-2),
                IsActive = true,
                IsDeleted = false,
                Name = "Kitchen",
                Slug = "kitchen",
                Order = 2,
                CreatedBy = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f"
            }
        );
    }
}
