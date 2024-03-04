namespace Sawoodamo.API.Features.Categories;

public sealed record CreateCategoryCommand(string Name, string Slug, int? Order) : IRequest<string>;

#region Validators

[ValidationOrder(1)]
public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage("Invalid order");

        RuleFor(x => x.Name)
            .MaximumLength(Constants.Category.NameMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Category.Name), Constants.Category.NameMinLength, Constants.Category.NameMaxLength))
            .NotNull()
                .WithMessage("Name is required")
            .NotEmpty()
                .WithMessage("Name is required");

        RuleFor(x => x.Slug)
            .Matches(RegexValidators.SlugValidatorRegex())
                .WithMessage("Invalid slug")
            .MaximumLength(Constants.Other.SlugMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Category.Slug), Constants.Slug.MinLength, Constants.Slug.MaxLength));
    }
}

[ValidationOrder(2)]
public sealed class CreateCategoryCommandAsyncValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandAsyncValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellationToken) => !await context.Categories.AnyAsync(x => x.Name == name, cancellationToken))
                .WithMessage(ErrorMessageGenerator.InUse(nameof(Category.Name)))
                .WithErrorCode("DuplicateName")
        .DependentRules(() =>
        {
            RuleFor(x => x.Slug)
                .MustAsync(async (slug, cancellationToken) => !await context.Categories.AnyAsync(x => x.Slug == slug, cancellationToken))
                    .WithMessage(ErrorMessageGenerator.InUse(nameof(Category.Slug)))
                    .WithErrorCode("DuplicateSlug");
        });
    }
}

#endregion

public sealed class CreateCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateCategoryCommand, string>
{
    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Slug = request.Slug,
            Order = request.Order,
        };

        await context.Categories.AddAsync(category, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        return category.Id;
    }
}