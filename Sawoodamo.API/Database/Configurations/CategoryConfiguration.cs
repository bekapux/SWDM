using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        #region Defaults

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => x.IsDeleted == false);

        #endregion

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).HasMaxLength(Constants.Category.NameMaxLength);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
