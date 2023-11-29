namespace Sawoodamo.API.Database.Entities.Utilities;

public abstract class BaseDomainEntity<TId> : IEntityWithId<TId>
{
    public TId Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool? IsActive { get; set; }
    public bool IsDeleted { get; set; }
}