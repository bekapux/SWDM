namespace Sawoodamo.API.Features.Categories;

public sealed record GetCategoriesQuery : IRequest<List<CategoryListItemDto>>;
public sealed record CategoryListItemDto(int Id, int? Order, string Name, string Slug) { }
public sealed class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("api/categories", async (ISender sender, CancellationToken token) =>
            Results.Ok(await sender.Send(new GetCategoriesQuery(), token)))

    .WithTags("Category");
}

public sealed class GetCategoriesQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetCategoriesQuery, List<CategoryListItemDto>>
{
    public async Task<List<CategoryListItemDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories
            .AsNoTracking()
            .Select(x => new CategoryListItemDto(x.Id, x.Order, x.Name, x.Slug))
            .ToListAsync(cancellationToken);

        return categories;
    }
}
