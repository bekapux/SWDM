namespace Sawoodamo.API.Features.ProductImages;

public sealed record CreateProductImageCommand(IFormFile File, int? ProductId, int? Order, bool? IsMainImage = false) : IRequest;

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
            .Must(order => order is null or > 0)
                .WithMessage("Invalid order");
    }
}

public sealed class CreateProductImageCommandHandler(SawoodamoDbContext context, IFileService fileService) : IRequestHandler<CreateProductImageCommand>
{
    public async Task Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.File.OpenReadStream();

        var url = await fileService.CreateFile(Guid.NewGuid().ToString(), stream);
        
        var image = new ProductImage
        {
            Url = url,
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
