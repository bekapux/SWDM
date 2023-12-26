namespace Sawoodamo.API.Features.Products;

public sealed record GetPinnedProductsQuery : IRequest<IEnumerable<ProductListItemDTO>>;

public sealed record GetPinnedProductsQueryHandler(SawoodamoDbContext Context) : IRequestHandler<GetPinnedProductsQuery, IEnumerable<ProductListItemDTO>>
{
    public async Task<IEnumerable<ProductListItemDTO>> Handle(GetPinnedProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await Context.Products
            .Where(x => x.IsPinned)
            .Select(x => new ProductListItemDTO
            {
                Discount = x.Discount,
                FullDescription = x.FullDescription,
                MainImageUrl = x.ProductImages!.FirstOrDefault(x => x.IsMainImage)!.Url,
                Name = x.Name,
                Order = x.Order,
                Price = x.Price,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug
            }).ToListAsync(cancellationToken);

        return products;
    }
}
