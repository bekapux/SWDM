using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        #region Defaults

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => x.IsDeleted == false);

        #endregion

        builder.Property(x => x.Name).HasMaxLength(Constants.Product.NameMaxLength).IsRequired();
        builder.Property(x => x.ShortDescription).HasMaxLength(Constants.Product.ShortDescriptionMaxLength).IsRequired();
        builder.Property(x => x.FullDescription).HasMaxLength(Constants.Product.FullDescriptionMaxLength);
        builder.Property(x => x.Slug).HasMaxLength(Constants.Product.FullDescriptionMaxLength);
        builder.Property(x => x.IsPinned).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CurrentPrice).HasPrecision(10, 2).IsRequired(true);
        builder.Property(x => x.OriginalPrice).HasPrecision(10, 2).IsRequired(true);
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
