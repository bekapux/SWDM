using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductSpecSeed : IEntityTypeConfiguration<ProductSpec>
{
    public void Configure(EntityTypeBuilder<ProductSpec> builder)
    {
        builder.HasData(
            new ProductSpec
            {
                Id = 1,
                ProductId = 1,
                SpecName = "Weight",
                SpecValue = "221g",
            },
            new ProductSpec
            {
                Id = 2,
                ProductId = 1,
                SpecName = "Display Size",
                SpecValue = "6.7 inches",
            },
            new ProductSpec
            {
                Id = 3,
                ProductId = 1,
                SpecName = "OS",
                SpecValue = "iOS 17",
            }
        );
    }
}
