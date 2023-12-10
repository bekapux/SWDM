namespace Sawoodamo.API.Features.Categories;

public sealed record CreateCategoryCommand(string Name, string Slug, int? Order) : IRequest<int>;

public sealed partial class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Constants.Category.CategoryNameMaxLength)
                .WithMessage($"Maximum length of the name should be {Constants.Category.CategoryNameMaxLength} symbols")
            .NotNull()
                .WithMessage("Name is required")
            .NotEmpty()
                .WithMessage("Name is required")
            .MustAsync(async (name, cancellation) =>
            {
                var result = await context.Categories.FirstOrDefaultAsync(x => x.Name == name, cancellation);
                return result is null;
            })
                .WithMessage("The name is already in use")
                .WithErrorCode("DuplicateName");

        RuleFor(x => x.Slug)
            .Matches(RegexValidators.SlugValidatorRegex())
                .WithMessage("Invalid slug")
            .MaximumLength(Constants.Other.SlugMaxLength)
                .WithMessage($"Maximum length of the slug should be {Constants.Other.SlugMaxLength} symbols")
            .MustAsync(async (slug, cancellation) =>
            {
                var result = await context.Categories.FirstOrDefaultAsync(x => x.Slug == slug, cancellation);
                return result is null;
            })
                .WithMessage("The slug is already in use")
                .WithErrorCode("DuplicateSlug");

        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage("Invalid order");
    }
}

public sealed class CreateCategoryCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Slug = request.Slug,
            Order = request.Order,
        };

        await context.Categories.AddAsync(category, CancellationToken.None);
        await context.SaveChangesAsync<int>(CancellationToken.None);

        return category.Id;
    }
}