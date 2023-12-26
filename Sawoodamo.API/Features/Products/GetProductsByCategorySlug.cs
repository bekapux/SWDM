namespace Sawoodamo.API.Features.Products;

public sealed record GetProductsByCategorySlugQuery(
    string Slug,
    int PageNumber = 1,
    int ItemsPerPage = 10
) : IRequest<PaginatedListResult<ProductListItemDTO>>;

public sealed class GetProductsByCategorySlugQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetProductsByCategorySlugQuery, PaginatedListResult<ProductListItemDTO>>
{
    public async Task<PaginatedListResult<ProductListItemDTO>> Handle(GetProductsByCategorySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);

        if (category is null)
            throw new NotFoundException(nameof(Category), request.Slug);

        var productsFinal = await context.ProductCategories
            .Include(x => x.Product)
            .Include(x => x.Category)
            .Include(x=> x.Product!.ProductImages)
            .Where(x => x.CategoryId == category.Id)
            .ToPaginatedListAsync(
                pageNumber: request.PageNumber,
                itemsPerPage: request.ItemsPerPage,
                mapper: productCategory => new ProductListItemDTO
                    {
                        Name = productCategory?.Product?.Name,
                        ShortDescription = productCategory?.Product?.ShortDescription,
                        FullDescription = productCategory?.Product?.FullDescription,
                        Slug = productCategory?.Product?.Slug ?? String.Empty,
                        MainImageUrl = productCategory?.Product?.ProductImages?.FirstOrDefault(x => x.IsMainImage)?.Url ?? productCategory?.Product?.ProductImages?.FirstOrDefault()?.Url,
                        Price = productCategory?.Product?.Price ?? int.MaxValue,
                        Discount = productCategory?.Product?.Discount ?? 0,
                        Order = productCategory?.Product?.Order ?? 0,
                    },
                cancellationToken: cancellationToken);

        return productsFinal;
    }
}

public sealed record ProductListItemDTO
{
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public int? Discount { get; set; }
    public decimal Price { get; set; }
    public string? Slug { get; set; }
    public int? Order { get; set; }
    public string? MainImageUrl { get; set; }
}