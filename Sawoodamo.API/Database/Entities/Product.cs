namespace Sawoodamo.API.Database.Entities;

public sealed class Product : BaseDomainEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string FullDescription { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }

    #region Nav Properties

    public List<ProductImage>? ProductImages { get; set; }
    public ICollection<ProductCategory>? ProductCategories { get; set; }

    #endregion
}

