namespace Sawoodamo.API.Features.Categories;

public sealed record CategoryDTO(string Id, string Slug, string Name, int? Order, bool? IsActive);

public sealed record GetCategoryQuery(string Id) : IRequest<CategoryDTO>;

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
