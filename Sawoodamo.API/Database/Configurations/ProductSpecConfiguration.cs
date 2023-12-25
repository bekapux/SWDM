using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public sealed class ProductSpecConfiguration : IEntityTypeConfiguration<ProductSpec>
{
    public void Configure(EntityTypeBuilder<ProductSpec> builder)
    {
        #region Defaults

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => x.IsDeleted == false);

        #endregion

        builder.Property(x=> x.SpecValue).HasMaxLength(Constants.ProductSpec.SpecValueMaxLength).IsRequired();
        builder.Property(x=> x.SpecName).HasMaxLength(Constants.ProductSpec.SpecNameMaxLength).IsRequired();

        builder.HasIndex(x => x.ProductId);
    }
}
