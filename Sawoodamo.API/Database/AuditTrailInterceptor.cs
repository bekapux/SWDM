using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Sawoodamo.API.Database;

public class AuditTrailInterceptor : SaveChangesInterceptor
{
    private readonly HashSet<Type> _auditedEntityTypes = [typeof(Product), typeof(Category)];

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var entries = GetAuditableEntries(eventData);
        foreach (var entry in entries)
        {
            var auditLog = CreateAuditLog(entry);
            eventData.Context?.Set<AuditTrail>().Add(auditLog);
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var entries = GetAuditableEntries(eventData);
        foreach (var entry in entries)
        {
            var auditLog = CreateAuditLog(entry);
            await eventData.Context!.Set<AuditTrail>().AddAsync(auditLog, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static AuditTrail CreateAuditLog(EntityEntry entry)
    {
        string SerializeEntity(object entity)
        {
            return JsonSerializer.Serialize(entity);
        }

        string SerializeUpdatedProperties(EntityEntry entry)
        {
            var properties = entry.Properties
                .Where(p => p.IsModified)
                .ToDictionary(p => p.Metadata.Name, p => new { Old = p.OriginalValue, New = p.CurrentValue });

            return JsonSerializer.Serialize(properties);
        }

        var oldValue = entry.State == EntityState.Modified ? SerializeUpdatedProperties(entry) : string.Empty;
        var newValue = entry.State == EntityState.Added ? SerializeEntity(entry.Entity) : oldValue;

        var modifiedProperties = entry.State == EntityState.Modified    ? string.Join(",", entry.Properties
                                                                                                .Where(p => p.IsModified)
                                                                                                .Select(p => p.Metadata.Name)) 
                                                                        : string.Empty;

        return new AuditTrail
        {
            EntityId = entry.Entity.GetType().Name,
            EntityType = entry.Entity.GetType().Name,
            Action = entry.State switch { EntityState.Added => "Create", EntityState.Modified => "Update", EntityState.Deleted => "Delete", _ => "Unknown" },
            OldValue = oldValue,
            NewValue = newValue,
            ModifiesFieldsJoined = modifiedProperties,
            Timestamp = DateTime.UtcNow
        };
    }

    private IEnumerable<EntityEntry> GetAuditableEntries(DbContextEventData eventData)
    {
        return eventData.Context?.ChangeTracker
            .Entries()
            .Where(e =>
                _auditedEntityTypes.Contains(e.Entity.GetType()) &&
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
            .ToList() ?? Enumerable.Empty<EntityEntry>();
    }
}
