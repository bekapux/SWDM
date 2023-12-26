namespace Sawoodamo.API.Database.Entities;

public sealed class Product : BaseDomainEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string FullDescription { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }
    public decimal Price { get; set; }
    public int? Discount { get; set; }
    public bool IsPinned { get; set; }

    #region Nav Properties

    public List<ProductImage>? ProductImages { get; set; }
    public ICollection<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();
    public ICollection<ProductSpec>? ProductSpecs { get; set; }

    #endregion
}
