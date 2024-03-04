namespace Sawoodamo.API.Database.Entities.Utilities;

public abstract class BaseDomainEntity : IEntityWithId
{
    public string Id { get; set; }
    public bool? IsActive { get; set; }
    public bool IsDeleted { get; set; }
}