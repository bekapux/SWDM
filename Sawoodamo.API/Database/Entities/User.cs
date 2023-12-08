namespace Sawoodamo.API.Database.Entities;

public sealed class User : IdentityUser, IEntityWithId<string>
{
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public DateTime DateRegistered { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsActive { get; set; }
    public bool IsAdmin { get; set; }
}
