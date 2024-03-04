namespace Sawoodamo.API.Database.Entities;

public sealed class ProductImage : BaseDomainEntity
{
    public string Url { get; init; } = String.Empty;
    public string? ProductId { get; set; }
    public bool IsMainImage { get; set; }
    public int? Order { get; set; }

    #region Nav Properties

    public Product? Product { get; set; }

    #endregion
}
