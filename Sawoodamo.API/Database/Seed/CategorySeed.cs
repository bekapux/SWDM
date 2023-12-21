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
                IsActive = true,
                IsDeleted = false,
                Name = "Electronics",
                Slug = "electronics",
                Order = 1,
            },
            new Category
            {
                Id = 2,
                IsActive = true,
                IsDeleted = false,
                Name = "Kitchen",
                Slug = "kitchen",
                Order = 2,
            }
        );
    }
}
