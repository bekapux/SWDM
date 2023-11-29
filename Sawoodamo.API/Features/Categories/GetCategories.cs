using Sawoodamo.API.Features.Categories.Dto;

namespace Sawoodamo.API.Features.Categories;

public sealed class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("api/categories", async (ISender sender, CancellationToken token) =>
            Results.Ok(await sender.Send(new GetCategoriesQuery(), token)));
}

public sealed record GetCategoriesQuery : IRequest<List<CategoryListItemDto>>;

public sealed class GetCategoriesQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetCategoriesQuery, List<CategoryListItemDto>>
{
    public async Task<List<CategoryListItemDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories
            .Select(x => new CategoryListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                Slug = x.Slug
            })
            .ToListAsync(cancellationToken);

        return categories;
    }
}
