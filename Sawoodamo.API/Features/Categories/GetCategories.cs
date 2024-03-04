namespace Sawoodamo.API.Features.Categories;

public sealed record GetCategoriesQuery : IRequest<List<CategoryListItemDto>?>;

public sealed record CategoryListItemDto(string Id, int? Order, string Name, string Slug) { }

public sealed class GetCategoriesQueryHandler(SawoodamoDbContext context, IMemoryCache memoryCache) : IRequestHandler<GetCategoriesQuery, List<CategoryListItemDto>?>
{
    private readonly TimeSpan CacheTime = TimeSpan.FromMinutes(5);

    public async Task<List<CategoryListItemDto>?> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!memoryCache.TryGetValue(nameof(GetCategoriesQuery), out List<CategoryListItemDto>? cachedCategories))
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Select(x => new CategoryListItemDto(x.Id, x.Order, x.Name, x.Slug))
                .ToListAsync(cancellationToken);

            memoryCache.Set(nameof(GetCategoriesQuery), categories, CacheTime);

            return categories;
        }

        return cachedCategories;
    }
}
