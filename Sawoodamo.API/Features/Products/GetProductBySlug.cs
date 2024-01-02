namespace Sawoodamo.API.Features.Products;

public sealed record GetProductBySlugQuery(string Slug) : IRequest<ProductDto>;

public sealed class GetProductBySlugQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetProductBySlugQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .AsNoTracking()
            .Select(x => new ProductDto
            {
                FullDescription = x.FullDescription,
                Slug = x.Slug,
                Name = x.Name,
                Id = x.Id,
                ProductImages = x.ProductImages!.Select(x => new ProductImageDto
                {
                    Id = x.Id,
                    Url = x.Url,
                    Order = x.Order
                }),
                Price = x.Price,
                ShortDescription = x.ShortDescription,
                ProductCategories = x.ProductCategories.Select(pc => pc.Category.Name),
                ProductSpecs = x.ProductSpecs.Select(ps => new ProductSpecDTO(ps.SpecName,ps.SpecValue))
            }).FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Slug);

        return product!;
    }
}

public sealed class ProductDto
{
    public int Id { get; set; }
    public string? Slug { get; set; }
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<ProductImageDto>? ProductImages { get; set; }
    public IEnumerable<string>? ProductCategories { get; set; }
    public IEnumerable<ProductSpecDTO>? ProductSpecs { get; set; }
}

public sealed record ProductImageDto
{
    public string? Url { get; set; }
    public int Id { get; set; }
    public int? Order { get; set; }
}

public sealed record ProductSpecDTO(string SpecName, string SpecValue);