using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        #region Defaults

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedBy).HasMaxLength(200).IsRequired();
        builder.Property(x => x.LastModifiedBy).HasMaxLength(200);
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => x.IsDeleted == false);

        #endregion

        builder.Property(x => x.Name).HasMaxLength(Constants.Product.NameMaxLength).IsRequired();
        builder.Property(x => x.ShortDescription).HasMaxLength(Constants.Product.ShortDescriptionMaxLength).IsRequired();
        builder.Property(x => x.FullDescription).HasMaxLength(Constants.Product.FullDescriptionMaxLength);
        builder.Property(x => x.Slug).HasMaxLength(Constants.Product.FullDescriptionMaxLength);

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
