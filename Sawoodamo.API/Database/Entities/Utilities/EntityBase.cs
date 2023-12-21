namespace Sawoodamo.API.Database.Entities.Utilities;

public abstract class BaseDomainEntity<TId> : IEntityWithId<TId>
{
    public TId Id { get; set; }
    public bool? IsActive { get; set; }
    public bool IsDeleted { get; set; }
}