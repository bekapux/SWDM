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
            .Where(x => x.CategoryId == category.Id)
            .ToPaginatedListAsync(
                pageNumber: request.PageNumber,
                itemsPerPage: request.ItemsPerPage,
                mapper: productCategory => new ProductListItemDTO
                    {
                        Slug = productCategory.Product.Slug ?? String.Empty,
                        FullDescription = productCategory.Product?.FullDescription,
                        Name = productCategory.Product!.Name,
                        Order = productCategory.Product.Order,
                        ShortDescription = productCategory.Product.ShortDescription,
                        Base64Value = productCategory.Product.ProductImages?.FirstOrDefault(x => x.IsMainImage)?.Base64Value
                    },
                cancellationToken: cancellationToken);

        return productsFinal;
    }
}

public sealed record ProductListItemDTO
{
    public string Name { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string? FullDescription { get; set; }
    public string Slug { get; set; } = String.Empty;
    public int? Order { get; set; }
    [Base64String] public string? Base64Value { get; set; } = String.Empty;
}