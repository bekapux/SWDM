namespace Sawoodamo.API.Features.ProductImages;

public sealed record ReorderProductImagesCommand(string ProductId, Dictionary<string, int> ImageOrder) : IRequest;

#region Validators

public sealed class ReorderProductImagesCommandValidator : AbstractValidator<ReorderProductImagesCommand>
{
    public ReorderProductImagesCommandValidator()
    {
        RuleFor(command => command.ImageOrder)
            .NotEmpty()
                .WithMessage("ImageOrder cannot be empty.")
            .Must(BeUnique)
                .WithMessage("All keys and values must be unique.");
    }

    private static bool BeUnique(Dictionary<string, int> dictionary) =>
         dictionary.Count == dictionary.Values.ToHashSet().Count;
}

#endregion

public sealed class ReorderProductImagesCommandHandler(SawoodamoDbContext context) : IRequestHandler<ReorderProductImagesCommand>
{
    public async Task Handle(ReorderProductImagesCommand request, CancellationToken cancellationToken)
    {
        var productWithImages = await context.Products
            .Include(p => p.ProductImages ?? Enumerable.Empty<ProductImage>())
            .FirstOrDefaultAsync(p => p.Id == request.ProductId);

        if (productWithImages is null)
            throw new NotFoundException(nameof(ProductImage), request.ProductId);

        if (productWithImages.ProductImages?.Count == 0) return;

        foreach (var image in productWithImages.ProductImages ?? Enumerable.Empty<ProductImage>())
        {
            bool hasValue = request.ImageOrder.TryGetValue(image.Id, out var imageOrder);
            if (hasValue)
            {
                image.Order = imageOrder;
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
