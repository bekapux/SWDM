namespace Sawoodamo.API.Database.Entities;

public sealed class ProductCategory
{
    public string? CategoryId { get; set; }
    public string? ProductId { get; set; }
    public bool IsDeleted { get; set; }

    #region Nav Properties

    public Product? Product { get; set; }
    public Category? Category { get; set; }

    #endregion
}
