namespace Sawoodamo.API.Features.Products;

public sealed class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapDelete("api/product/{id:int}", async (ISender sender, int id, CancellationToken token) =>
            await sender.Send(new DeleteProductCommand(id), token)).WithTags("Product");
}

public sealed record DeleteProductCommand(int ProductId) : IRequest;

public sealed class DeleteProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product is null || product.IsDeleted)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        product.IsDeleted = true;

        await context.SaveChangesAsync(cancellationToken);
    }
}
