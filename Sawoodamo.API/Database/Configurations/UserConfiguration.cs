using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        #region Defaults

        builder.Property(x => x.Id).HasMaxLength(Constants.Other.GuidMaxLength);
        builder.Property(x => x.DateRegistered).HasDefaultValueSql("getdate()");
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.IsAdmin).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(Constants.Other.PhoneNumberMaxLength);
        builder.HasQueryFilter(x => x.IsDeleted == false);

        #endregion

        builder.Property(x => x.Firstname).HasMaxLength(Constants.User.NameMaxLength).IsRequired();
        builder.Property(x => x.Lastname).HasMaxLength(Constants.User.LastNameMaxLength).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(Constants.User.EmailMaxLength).IsRequired();
        builder.Property(x => x.EmailConfirmed).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();

        #region Index

        builder.HasIndex(x => x.Email);

        #endregion
    }
}
