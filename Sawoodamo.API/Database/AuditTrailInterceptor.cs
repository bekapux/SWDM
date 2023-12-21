using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Sawoodamo.API.Database;

public class AuditTrailInterceptor : SaveChangesInterceptor
{
    private readonly HashSet<Type> _auditedEntityTypes = [typeof(Product), typeof(Category)];

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var entries = GetAuditableEntries(eventData);
        var sessionService = eventData.Context!.GetService<ISessionService>();
        var userId = sessionService.CurrentUserId();
        foreach (var entry in entries)
        {
            var auditLog = CreateAuditLog(entry, userId);
            eventData.Context?.Set<AuditTrail>().Add(auditLog);
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var entries = GetAuditableEntries(eventData);

        var sessionService = eventData.Context!.GetService<ISessionService>();

        var userId = sessionService.CurrentUserId();

        foreach (var entry in entries)
        {
            var auditLog = CreateAuditLog(entry, userId);
            await eventData.Context!.Set<AuditTrail>().AddAsync(auditLog, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static AuditTrail CreateAuditLog(EntityEntry entry, string? userId)
    {
        string oldValue = string.Empty;
        string newValue = string.Empty;

        switch (entry.State)
        {
            case EntityState.Added:
                newValue = SerializeEntity(entry.Entity);
                break;
            case EntityState.Modified:
                oldValue = SerializeProperties(entry, false);
                newValue = SerializeProperties(entry, true);
                break;
            case EntityState.Deleted:
                oldValue = SerializeProperties(entry, false);
                break;
        }

        var modifiedProperties = entry.State == EntityState.Modified    
            ? string.Join(",", entry.Properties
                                    .Where(p => p.IsModified)
                                    .Select(p => p.Metadata.Name)) 
            : string.Empty;

        return new AuditTrail
        {
            EntityId = entry.State == EntityState.Modified || entry.State == EntityState.Deleted ? GetEntityId(entry) : null,
            EntityType = entry.Entity.GetType().Name,
            Action = entry.State switch { EntityState.Added => "Create", EntityState.Modified => "Update", EntityState.Deleted => "Delete", _ => "Unknown" },
            OldValue = oldValue,
            NewValue = newValue,
            ModifiesFieldsJoined = modifiedProperties,
            Timestamp = DateTime.UtcNow,
            UserId = userId
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

    private static string SerializeEntity(object entity) => JsonSerializer.Serialize(entity);

    private static string SerializeProperties(EntityEntry entry, bool serializeNewValues)
    {
        var properties = entry.Properties
            .ToDictionary(p => p.Metadata.Name,
                          p => serializeNewValues ? p.CurrentValue : p.OriginalValue);

        return JsonSerializer.Serialize(properties);
    }

    private static string? GetEntityId(EntityEntry entry)
    {
        var keyName = entry.Metadata.FindPrimaryKey()?.Properties
                        .Select(x => x.Name)
                        .FirstOrDefault();

        return keyName != null ? entry.Property(keyName).CurrentValue?.ToString() : String.Empty;
    }
}
