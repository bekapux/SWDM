namespace Sawoodamo.API.Features.Categories;

public sealed record UpdateCategoryCommand(int Id, string Slug, int? Order, string Name) : IRequest;

public sealed class UpdateCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x=> x.Id == request.Id, cancellationToken) 
            ?? throw new NotFoundException(nameof(Category), request.Id);

        category.Slug = request.Slug;
        category.Order = request.Order;
        category.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);
    }
}
