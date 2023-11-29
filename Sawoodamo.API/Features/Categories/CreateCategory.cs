namespace Sawoodamo.API.Features.Categories;

public sealed class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/category", async (CreateCategoryCommand command, ISender sender) => Results.Ok(await sender.Send(command)));
    }
}

public sealed record CreateCategoryCommand(
    string Name,
    string Slug,
    int Order)
: IRequest<int>;

public sealed class CreateCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Slug = request.Slug,
            Order = request.Order,
        };

        await context.Categories.AddAsync(category, CancellationToken.None);
        await context.SaveChangesAsync<int>(CancellationToken.None);

        return category.Id;
    }
}
