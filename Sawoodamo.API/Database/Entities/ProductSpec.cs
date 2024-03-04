namespace Sawoodamo.API.Database.Entities;

public sealed class ProductSpec : BaseDomainEntity
{
    public string ProductId { get; set; }
    public string? SpecName { get; set; }
    public string? SpecValue { get; set; }
}
