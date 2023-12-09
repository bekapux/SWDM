
namespace Sawoodamo.API.Features.Products;

public sealed record AddImageToProductCommand(int ProductId, int OrderId, bool? IsMainImage, int? Order, [Base64String] string Base64Value) : IRequest;

public sealed class AddImageToProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<AddImageToProductCommand>
{
    public Task Handle(AddImageToProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
