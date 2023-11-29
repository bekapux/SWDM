
namespace Sawoodamo.API.Features.Products;


public sealed class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)=>
        app.MapDelete("product/{id:int}", async (ISender sender, int id, CancellationToken token) =>
            await sender.Send(new DeleteProductCommand(id)));
}

public sealed record DeleteProductCommand(int ProductId) : IRequest;

public sealed class DeleteProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId);

        if (product is null) return;

        product.IsDeleted = true;

        await context.SaveChangesAsync();
    }
}
