namespace Sawoodamo.API.Features.ProductSpecs;

public sealed record DeleteProductSpecCommand (int SpecId) : IRequest;

public sealed class DeleteProductSpecCommandHandler(SawoodamoDbContext context) : IRequestHandler<DeleteProductSpecCommand>
{
    public async Task Handle(DeleteProductSpecCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await context.ProductSpecs
            .Where(x => x.Id == request.SpecId)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows is 0)
            throw new NotFoundException(nameof(ProductSpec), request.SpecId);
    }
}
