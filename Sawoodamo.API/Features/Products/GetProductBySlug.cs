namespace Sawoodamo.API.Features.Products;

public sealed record GetProductBySlugQuery(string Slug) : IRequest<ProductDto>;

public sealed class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("api/product/{slug}", async (ISender sender, string slug, CancellationToken token) =>
            await sender.Send(new GetProductBySlugQuery(slug), token))
    .WithTags("Product");
}

public sealed class GetProductBySlugQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetProductBySlugQuery, ProductDto>
{
    public Task<ProductDto> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = context.Products
            .AsNoTracking()
            .Include(x => x.ProductImages).AsSplitQuery()
            .Select(x => new ProductDto
            {
                FullDescription = x.FullDescription,
                Slug = x.Slug,
                Name = x.Name,
                Id = x.Id,
                ProductImages = x.ProductImages!.Select(x => new ProductImageDto
                {
                    Id = x.Id,
                    Base64Value = x.Base64Value,
                    Order = x.Order
                }),
                ShortDescription = x.ShortDescription,
            }).FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Slug);
        }

        return product!;
    }
}

public sealed class ProductDto()
{
    public int Id { get; set; }
    public string? Slug { get; set; }
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public IEnumerable<ProductImageDto>? ProductImages { get; set; }
}

public sealed class ProductImageDto
{
    [Base64String]
    public string? Base64Value { get; set; }
    public int Id { get; set; }
    public int? Order { get; set; }
}