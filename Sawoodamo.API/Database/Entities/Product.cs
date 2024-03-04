namespace Sawoodamo.API.Database.Entities;

public sealed class Product : BaseDomainEntity
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string FullDescription { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }
    public required decimal OriginalPrice { get; set; }
    public required decimal CurrentPrice { get; set; }
    public bool IsPinned { get; set; }
    public byte DiscountPercent => (byte)Math.Round((OriginalPrice - CurrentPrice) / OriginalPrice * 100);

    #region Nav Properties

    public List<ProductImage>? ProductImages { get; set; }
    public ICollection<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();
    public ICollection<ProductSpec>? ProductSpecs { get; set; }

    #endregion
}
