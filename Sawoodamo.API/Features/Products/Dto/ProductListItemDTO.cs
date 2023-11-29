namespace Sawoodamo.API.Features.Products.Dto;

public class ProductListItemDTO
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string? FullDescription { get; set; }
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }
    public string CategoryName { get; set; } = String.Empty;
}
