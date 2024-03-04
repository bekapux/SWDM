using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductCategorySeed : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasData(
            new ProductCategory
            {
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                CategoryId = "cb8b63ea-6b7c-4a3c-a926-eaf0ca50b575",
            },
            new ProductCategory
            {
                ProductId = "6529374c-2166-4727-95ac-0c623aa21642",
                CategoryId = "cb8b63ea-6b7c-4a3c-a926-eaf0ca50b576",
            }
        );
    }
}
