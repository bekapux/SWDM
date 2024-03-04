using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public sealed class ProductSpecSeed : IEntityTypeConfiguration<ProductSpec>
{
    public void Configure(EntityTypeBuilder<ProductSpec> builder)
    {
        builder.HasData(
            new ProductSpec
            {
                Id = "bcafcbb7-0e61-4339-9528-468610385b93",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                SpecName = "Weight",
                SpecValue = "221g",
            },
            new ProductSpec
            {
                Id = "996b5fff-614e-49bb-8702-f292d9d2ec62",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                SpecName = "Display Size",
                SpecValue = "6.7 inches",
            },
            new ProductSpec
            {
                Id = "77306671-19be-4900-854a-cc5bd8a571f5",
                ProductId = "16e4437a-1c20-4bf4-9ca8-4273db759c71",
                SpecName = "OS",
                SpecValue = "iOS 17",
            }
        );
    }
}
