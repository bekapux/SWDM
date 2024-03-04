namespace Sawoodamo.API.Database.Entities;

public sealed class CartItem
{
    public string? Id { get; set; }
    public string? ProductId { get; set; }
    public int Quantity { get; set; }
    public string? UserId { get; set; }

    #region Nav Properties

    public Product? Product { get; set; }
    public User? User { get; set; }

    #endregion
}
