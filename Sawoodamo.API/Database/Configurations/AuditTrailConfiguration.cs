using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder.Property(x => x.EntityType).HasMaxLength(Constants.AuditTrails.EntityTypeMaxLength).IsRequired();
        //builder.Property(x => x.UserId).HasMaxLength(Constants.Other.IdMaxLength).IsRequired(false);
        builder.Property(x => x.Timestamp).HasMaxLength(Constants.AuditTrails.TimeStampMaxLength).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(Constants.AuditTrails.OperationTypeMaxLength).IsRequired();
        builder.Property(x => x.EntityId).HasMaxLength(Constants.Other.IdMaxLength).IsRequired();

        builder.HasIndex(x => x.EntityType);
    }
}
