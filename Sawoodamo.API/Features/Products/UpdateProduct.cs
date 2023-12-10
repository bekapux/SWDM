
namespace Sawoodamo.API.Features.Products;

public sealed record UpdateProductCommand(
    int ProductId,
    string Name,
    string ShortDescription,
    string? FullDescription,
    string Slug,
    int? Order,
    int CategoryId) : IRequest;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.ShortDescription)
            .NotNull()
            .NotEmpty()
                .WithMessage("Short description is required")
            .MaximumLength(Constants.Product.ProductShortDescriptionMaxLength)
                .WithMessage($"Full description should not be more than {Constants.Product.ProductShortDescriptionMaxLength} characters");

        RuleFor(x => x.FullDescription)
            .MaximumLength(Constants.Product.ProductFullDescriptionMaxLength)
                .WithMessage($"Full description should not be more than {Constants.Product.ProductFullDescriptionMaxLength} characters");

        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage("Invalid order");

        RuleFor(x => x.Name)
            .MaximumLength(Constants.Product.ProductNameMaxLength)
                .WithMessage($"Maximum length of the name should be {Constants.Product.ProductNameMaxLength} symbols")
            .NotNull()
                .WithMessage("Name is required")
            .NotEmpty()
                .WithMessage("Name is required")
            .MustAsync(async (name, cancellation) =>
            {
                var result = await context.Products.FirstOrDefaultAsync(x => x.Name == name, cancellation);
                return result is null;
            })
                .WithMessage("The name is already in use")
                .WithErrorCode("DuplicateName");

        RuleFor(x => x.CategoryId)
            .MustAsync(async (categoryId, token) =>
            {
                bool categoryExists = await context.Categories.AnyAsync(x => x.Id == categoryId, token);
                return categoryExists;
            })
                .WithMessage("Invalid category")
                .WithErrorCode("CategoryIdValidity");

        RuleFor(x => x.Slug)
            .MustAsync(async (product, slug, currentProductId) =>
            {
                bool slugExists = await context.Products.AnyAsync(x => x.Slug == slug && x.Id != product.ProductId);
                return !slugExists;
            })
                .WithMessage("The provided slug is already in use by another product.")
                .WithErrorCode("CategoryIdValidity");
    }
}

public sealed class UpdateProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Product), request.ProductId);

        product.Name = request.Name!;
        product.ShortDescription = request.ShortDescription!;
        product.FullDescription = request.FullDescription!;
        product.Slug = request.Slug!;
        product.CategoryId = request.CategoryId;
        product.Order = request.Order;

        await context.SaveChangesAsync(cancellationToken);
    }
}
