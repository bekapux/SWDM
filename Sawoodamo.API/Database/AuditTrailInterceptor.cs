using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Sawoodamo.API.Database;

public class AuditTrailInterceptor : SaveChangesInterceptor
{
    private readonly HashSet<Type> _auditedEntityTypes = [typeof(Product), typeof(Category)];

    private readonly Dictionary<Type, HashSet<string>> _ignoredProperties = new()
    {
        { typeof(Category), new HashSet<string> { nameof(Category.Slug), nameof(Category.Name) } }
    };

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

    private AuditTrail CreateAuditLog(EntityEntry entry, string? userId)
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

        var entityType = entry.Entity.GetType();

        var propertiesToIgnore = _ignoredProperties.TryGetValue(entityType, out HashSet<string>? value) ? value : [];

        var modifiedProperties = entry.State == EntityState.Modified
            ? string.Join(",", entry.Properties
                                    .Where(p => p.IsModified && !propertiesToIgnore.TryGetValue(p.Metadata.Name, out var _))
                                    .Select(p => p.Metadata.Name))
            : string.Empty;

        return new AuditTrail
        {
            EntityId = entry.State == EntityState.Modified || entry.State == EntityState.Deleted ? GetEntityId(entry) : null,
            EntityType = entityType.Name,
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
                .Where(e => !AllChangesIgnored(e))
            .ToList() ?? Enumerable.Empty<EntityEntry>();
    }

    private static string SerializeEntity(object entity) => JsonSerializer.Serialize(entity);

    private bool AllChangesIgnored(EntityEntry entry)
    {
        var entityType = entry.Entity.GetType();
        var propertiesToIgnore = _ignoredProperties.TryGetValue(entityType, out HashSet<string>? value) ? value : [];

        return entry.State == EntityState.Modified &&
               entry.Properties
               .Where(p => p.IsModified)
               .All(p => propertiesToIgnore.Contains(p.Metadata.Name));
    }

    private string SerializeProperties(EntityEntry entry, bool serializeNewValues)
    {
        var entityType = entry.Entity.GetType();

        var propertiesToIgnore = _ignoredProperties.ContainsKey(entityType) ? _ignoredProperties[entityType] : [];

        var properties = entry.Properties
            .Where(p => !propertiesToIgnore.Contains(p.Metadata.Name))
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
