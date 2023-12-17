namespace Sawoodamo.API.Database.Entities;

public class ProductCategory
{
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
    public bool IsDeleted { get; set; }

    #region Nav Properties

    public Product Product { get; set; }
    public Category Category { get; set; }

    #endregion
}
