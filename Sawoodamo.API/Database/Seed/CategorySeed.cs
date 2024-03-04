using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = "cb8b63ea-6b7c-4a3c-a926-eaf0ca50b575",
                IsActive = true,
                IsDeleted = false,
                Name = "Electronics",
                Slug = "electronics",
                Order = 1,
            },
            new Category
            {
                Id = "cb8b63ea-6b7c-4a3c-a926-eaf0ca50b576",
                IsActive = true,
                IsDeleted = false,
                Name = "Kitchen",
                Slug = "kitchen",
                Order = 2,
            }
        );
    }
}
