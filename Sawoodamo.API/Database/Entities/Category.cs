using Sawoodamo.API.Database.Entities.Utilities;

namespace Sawoodamo.API.Database.Entities;

public sealed class Category : BaseDomainEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public int? Order { get; set; }
    public string Slug { get; set; } = String.Empty;
}