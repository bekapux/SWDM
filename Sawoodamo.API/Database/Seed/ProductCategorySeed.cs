using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public class ProductCategorySeed : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasData(
            new ProductCategory
            {
                ProductId = 1,
                CategoryId = 1,
            },
            new ProductCategory
            {
                ProductId = 2,
                CategoryId = 2,
            }
        );
    }
}
