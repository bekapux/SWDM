namespace Sawoodamo.API.Database.Entities;

public sealed class ProductImage : BaseDomainEntity<int>
{
    public string Base64Value { get; set; } = String.Empty;
    public int ProductId { get; set; }
    public bool IsMainImage { get; set; }
    public bool Order { get; set; }

    #region Nav Properties

    public Product? Product { get; set; }

    #endregion
}
