
namespace Sawoodamo.API.Features.ProductSpecs;

public sealed record CreateProductSpecCommand(int ProductId, string SpecName, string SpecValue) : IRequest;

#region Validators

[ValidationOrder(1)]
public sealed class CreateProductSpecCommandValidator : AbstractValidator<CreateProductSpecCommand>
{
    public CreateProductSpecCommandValidator()
    {
        RuleFor(x => x.SpecName)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(ProductSpec.SpecName)))
            .MaximumLength(Constants.ProductSpec.SpecNameMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(ProductSpec.SpecName), Constants.ProductSpec.SpecNameMaxLength));

        RuleFor(x => x.SpecValue)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(ProductSpec.SpecValue)))
            .MaximumLength(Constants.ProductSpec.SpecValueMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(ProductSpec.SpecValue), Constants.ProductSpec.SpecValueMaxLength));

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Product.Id)));
    }
}

[ValidationOrder(2)]
public sealed class CreateProductSpecCommandAsyncValidator : AbstractValidator<CreateProductSpecCommand>
{
    public CreateProductSpecCommandAsyncValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.ProductId)
            .MustAsync(async (ProductId, cancellationToken) =>
                await context.Products.AnyAsync(x => x.Id == ProductId, cancellationToken))
                    .WithMessage(ErrorMessageGenerator.Invalid(nameof(Product.Id)))
                    .WithErrorCode("Does not exist");
    }
}

#endregion


public sealed class CreateProductSpecCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateProductSpecCommand>
{
    public async Task Handle(CreateProductSpecCommand request, CancellationToken cancellationToken)
    {
        var newSpec = new ProductSpec
        {
            ProductId = request.ProductId,
            SpecValue = request.SpecValue,
            SpecName = request.SpecName,
        };

        await context.ProductSpecs.AddAsync(newSpec, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
