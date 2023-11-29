namespace Sawoodamo.API.Features.Categories.Dto;

public sealed class CategoryListItemDto
{
    public int Id { get; set; }
    public int? Order { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
};
