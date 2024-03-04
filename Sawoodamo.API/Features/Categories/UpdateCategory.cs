namespace Sawoodamo.API.Features.Categories;

public sealed record UpdateCategoryCommand(string Id, string Slug, int? Order, string Name) : IRequest;

#region Validation

[ValidationOrder(1)]
public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Category.Id)));

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(Category.Name)))
            .MaximumLength(Constants.Category.NameMaxLength)
            .MinimumLength(Constants.Category.NameMinLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Category.Name), Constants.Category.NameMinLength, Constants.Category.NameMaxLength));

        RuleFor(x => x.Slug)
            .Matches(RegexValidators.SlugValidatorRegex())
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Category.Slug)))
            .MaximumLength(Constants.Other.SlugMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Category.Name), Constants.Slug.MinLength, Constants.Slug.MaxLength));

        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Category.Order)));
    }
}

[ValidationOrder(2)]
public sealed class UpdateCategoryCommandAsyncValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandAsyncValidator(SawoodamoDbContext context)
    {
        RuleFor(command => command)
            .MustAsync(async (command, cancellationToken) =>
            {
                var existingCategory = await context.Categories
                    .Where(category => category.Name == command.Name)
                    .FirstOrDefaultAsync(cancellationToken);
                return existingCategory == null || existingCategory.Id == command.Id;
            })
                .WithMessage(ErrorMessageGenerator.InUse(nameof(Category.Name)))
                .WithErrorCode(Constants.AsyncValidatorErrorCodes.DuplicateName)
        .DependentRules(() =>
        {
            RuleFor(command => command)
                .MustAsync(async (command, cancellationToken) =>
                {
                    var existingCategory = await context.Categories
                        .Where(c => c.Slug == command.Slug)
                        .FirstOrDefaultAsync(cancellationToken);
                    return existingCategory == null || existingCategory.Id == command.Id;
                })
                    .WithMessage(ErrorMessageGenerator.InUse(nameof(Category.Slug)))
                    .WithErrorCode(Constants.AsyncValidatorErrorCodes.DuplicateSlug);
        });
    }
}

#endregion

public sealed class UpdateCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Category), request.Id);

        category.Slug = request.Slug;
        category.Order = request.Order;
        category.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);
    }
}
