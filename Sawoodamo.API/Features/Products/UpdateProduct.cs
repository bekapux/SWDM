
using System.Threading;

namespace Sawoodamo.API.Features.Products;

public sealed record UpdateProductCommand(
    int Id,
    string Name,
    string ShortDescription,
    string? FullDescription,
    string Slug,
    int? Order,
    int CategoryId) : IRequest;

#region Validators

[ValidationOrder(1)]
public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ShortDescription)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(Product.ShortDescription)))
            .MaximumLength(Constants.Product.ShortDescriptionMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(Product.ShortDescription), Constants.Product.ShortDescriptionMaxLength));

        RuleFor(x => x.FullDescription)
            .MaximumLength(Constants.Product.FullDescriptionMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(Product.FullDescription), Constants.Product.FullDescriptionMaxLength));

        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Product.Order)));

        RuleFor(x => x.Name)
            .MaximumLength(Constants.Product.NameMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Product.Name), Constants.Product.NameMinLength, Constants.Product.NameMaxLength))
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(Product.Name)));
    }
}

[ValidationOrder(2)]
public sealed class UpdateProductCommandAsyncValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandAsyncValidator(SawoodamoDbContext context)
    {
        RuleFor(command => command)
            .MustAsync(async (command, cancellationToken) =>
            {
                var existingProduct = await context.Categories
                        .Where(c => c.Name == command.Name)
                        .FirstOrDefaultAsync(cancellationToken);
                return existingProduct == null || existingProduct.Id == command.Id;
            })
                .WithMessage(ErrorMessageGenerator.InUse(nameof(Product.Name)))
                .WithErrorCode(Constants.AsyncValidatorErrorCodes.DuplicateName)

        .DependentRules(() =>
        {
            RuleFor(command => command)
            .MustAsync(async (command, cancellationToken) =>
            {
                var existingProduct = await context.Categories
                        .Where(c => c.Slug == command.Slug)
                        .FirstOrDefaultAsync(cancellationToken);
                return existingProduct == null || existingProduct.Id == command.Id;
            })
                .WithMessage(ErrorMessageGenerator.InUse(nameof(Product.Slug)))
                .WithErrorCode(Constants.AsyncValidatorErrorCodes.DuplicateSlug);
        })

        .DependentRules(() =>
        {
            RuleFor(x => x.CategoryId)
                .MustAsync(async (id, cancellationToken) =>
                {
                    var categoryExists = await context.Categories
                            .AnyAsync(c => c.Id == id, cancellationToken);
                    return categoryExists;
                })
                    .WithMessage(ErrorMessageGenerator.InUse(nameof(Product.Slug)))
                    .WithErrorCode(Constants.AsyncValidatorErrorCodes.DuplicateSlug);
        });
    }
}

#endregion

public sealed class UpdateProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), request.Id);

        product.Name = request.Name!;
        product.ShortDescription = request.ShortDescription!;
        product.FullDescription = request.FullDescription!;
        product.Slug = request.Slug!;
        product.Order = request.Order;

        await context.SaveChangesAsync(cancellationToken);
    }
}
