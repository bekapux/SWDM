namespace Sawoodamo.API.Features.Categories;

public sealed record DeleteCategoryCommand(int Id) : IRequest;

public sealed class DeleteCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        product.IsDeleted = true;

        await context.SaveChangesAsync(cancellationToken);
    }
}
