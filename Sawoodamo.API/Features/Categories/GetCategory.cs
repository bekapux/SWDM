namespace Sawoodamo.API.Features.Categories;

public sealed record CategoryDTO(int Id, string Slug, string Name, int? Order, bool? IsActive);
public sealed record GetCategoryQuery(int Id) : IRequest<CategoryDTO>;
public sealed class GetCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("api/category/{id:int}", async (int id, ISender sender, CancellationToken token) =>
            Results.Ok(await sender.Send(new GetCategoryQuery(id), token)))

    .WithTags("Category");
}

public sealed class GetCategoryQueryHandler(SawoodamoDbContext context) : IRequestHandler<GetCategoryQuery, CategoryDTO>
{
    public async Task<CategoryDTO> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryDto = await context
            .Categories
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new CategoryDTO(x.Id, x.Slug, x.Name, x.Order, x.IsActive))
            .FirstOrDefaultAsync(cancellationToken);

        return categoryDto is null ? throw new NotFoundException(nameof(Category), request.Id) : categoryDto;
    }
}
