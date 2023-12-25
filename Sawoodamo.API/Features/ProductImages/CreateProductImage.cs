namespace Sawoodamo.API.Features.ProductImages;

public sealed record CreateProductImageCommand(IFormFile File, int? ProductId, int? Order, bool? IsMainImage = false) : IRequest;

#region Validator

[ValidationOrder(1)]
public sealed class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage("Valid product ID is required");

        RuleFor(x => x.Order)
            .Must(order => order is null or > 0)
                .WithMessage("Invalid order");
    }
}

[ValidationOrder(2)]
public sealed class CreateProductImageCommandAsyncValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandAsyncValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.ProductId)
            .MustAsync(async (productId, token) => await context.Products
                .AnyAsync(x => x.Id == productId, token))
                    .WithMessage("Product does not exist")
                    .WithErrorCode("InvalidProductId");
    }
}

#endregion

public sealed class CreateProductImageCommandHandler(SawoodamoDbContext context, IFileService fileService) : IRequestHandler<CreateProductImageCommand>
{
    public async Task Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.File.OpenReadStream();

        var imageUrl = await fileService.CreateFile(Guid.NewGuid().ToString(), stream, cancellationToken);
        
        var image = new ProductImage
        {
            Url = imageUrl,
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
