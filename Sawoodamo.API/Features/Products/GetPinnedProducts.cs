namespace Sawoodamo.API.Features.Products;

public sealed record GetPinnedProductsQuery : IRequest<IEnumerable<ProductListItemDTO>?>;

public sealed record GetPinnedProductsQueryHandler(SawoodamoDbContext Context, IMemoryCache memoryCache) : IRequestHandler<GetPinnedProductsQuery, IEnumerable<ProductListItemDTO>?>
{
    private readonly TimeSpan CacheTime = TimeSpan.FromMinutes(5);

    public async Task<IEnumerable<ProductListItemDTO>?> Handle(GetPinnedProductsQuery request, CancellationToken cancellationToken)
    {
        if (!memoryCache.TryGetValue(nameof(GetPinnedProductsQuery), out IEnumerable<ProductListItemDTO>? cachedCategories))
        {
            var products = await Context.Products
                .Where(x => x.IsPinned)
                .Select(x => new ProductListItemDTO
                {
                    CurrentPrice = x.CurrentPrice,
                    FullDescription = x.FullDescription,
                    MainImageUrl = x.ProductImages!.FirstOrDefault(x => x.IsMainImage)!.Url,
                    Name = x.Name,
                    Order = x.Order,
                    OriginalPrice = x.OriginalPrice,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug
                }).ToListAsync(cancellationToken);

            memoryCache.Set(nameof(GetPinnedProductsQuery), products, CacheTime);

            return products;
        }

        return cachedCategories;
    }
}
