namespace Sawoodamo.API.Features.Products;

public sealed record CreateProductCommand(
    string Name,
    string ShortDescription,
    string FullDescription,
    string Slug,
    int? Order
) : IRequest<int>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage("Invalid order");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
                .WithMessage("Name is required")
            .MaximumLength(Constants.Product.NameMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Product.Name), Constants.Product.NameMinLength, Constants.Product.NameMaxLength));


    }
}

public class CreateProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = request.Name,
            FullDescription = request.FullDescription,
            ShortDescription = request.ShortDescription,
            Slug = request.Slug,
            Order = request.Order
        };

        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
