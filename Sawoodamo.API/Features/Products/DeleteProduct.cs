namespace Sawoodamo.API.Features.Products;

public sealed record DeleteProductCommand(int ProductId) : IRequest;

public sealed class DeleteProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await context.Products
            .Where(x => x.Id == request.ProductId)
            .ExecuteUpdateAsync(x=> x.SetProperty(y=> y.IsDeleted, true), cancellationToken);

        if (affectedRows is 0)
            throw new NotFoundException(nameof(Product), request.ProductId);
    }
}
