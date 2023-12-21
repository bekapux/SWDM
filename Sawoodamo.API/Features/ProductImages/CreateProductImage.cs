using Sawoodamo.API.Services.Abstractions;

namespace Sawoodamo.API.Features.ProductImages;

public sealed record CreateProductImageCommand(int? ProductId, int OrderId, bool? IsMainImage, int? Order, [Base64String] string Base64Value) : IRequest;

public sealed class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage("Valid product ID is required")
            .MustAsync(async (productId, token) =>
            {
                var productExists = await context.Products.AnyAsync(x=> x.Id == productId, token);
                return productExists;
            })
                .WithMessage("Product does not exist")
                .WithErrorCode("ProductExists");

        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage("Invalid order");
    }
}

public sealed class CreateProductImageCommandHandler(SawoodamoDbContext context, ISessionService sessionService) : IRequestHandler<CreateProductImageCommand>
{
    public async Task Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = new ProductImage
        {
            DateCreated = DateTime.UtcNow,
            Base64Value = request.Base64Value,
            IsActive = true,
            IsDeleted = false,
            IsMainImage = request.IsMainImage ?? false,
            Order = request.Order,
            ProductId = request.ProductId!.Value,
        };

        await context.ProductImages.AddAsync(image, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
