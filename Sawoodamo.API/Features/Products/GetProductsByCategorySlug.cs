namespace Sawoodamo.API.Features.Products;

public sealed record GetProductsByCategorySlugQuery(
    string CategorySlug,
    int PageNumber = 1,
    int ItemsPerPage = 10
) : IRequest<PaginatedListResult<ProductListItemDTO>>;

public sealed class GetProductsByCategorySlugQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetProductsByCategorySlugQuery, PaginatedListResult<ProductListItemDTO>>
{
    public async Task<PaginatedListResult<ProductListItemDTO>> Handle(GetProductsByCategorySlugQuery request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x=> x.ProductImages)
            .Where(x => x.Category != null && x.Category.Slug == request.CategorySlug)
            .ToPaginatedListAsync(
                pageNumber: request.PageNumber,
                itemsPerPage: request.ItemsPerPage,
                product => new ProductListItemDTO
                {
                    CategoryName = product.Category?.Name ?? String.Empty,
                    Slug = product?.Slug ?? String.Empty,
                    FullDescription = product?.FullDescription,
                    Name = product!.Name,
                    Order = product.Order,
                    ShortDescription = product.ShortDescription,
                    Base64Value = product.ProductImages?.FirstOrDefault(x=> x.IsMainImage)?.Base64Value
                },
                cancellationToken
            );

        products.ResultList = products.ResultList?.OrderBy(x => x.Order).ToList();

        return products;
    }
}

public sealed record ProductListItemDTO
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string? FullDescription { get; set; }
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }
    public string CategoryName { get; set; } = String.Empty;
    [Base64String] public string? Base64Value { get; set; } = String.Empty;
}