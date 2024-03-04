namespace Sawoodamo.API.Database.Entities;

public sealed class Category : BaseDomainEntity
{
    public string Name { get; set; } = String.Empty;
    public int? Order { get; set; }
    public string Slug { get; set; } = String.Empty;

    #region Nav Properties

    public ICollection<ProductCategory>? ProductCategories { get; set; }

    #endregion
}