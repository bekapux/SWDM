namespace Sawoodamo.API.Database.Entities;

public sealed class ProductSpec : BaseDomainEntity<int>
{
    public int ProductId { get; set; }
    public string? SpecName { get; set; }
    public string? SpecValue { get; set; }
}
