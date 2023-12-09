using System.Text.RegularExpressions;

public sealed record CreateCategoryCommand(
    string Name,
    string Slug,
    int Order)
: IRequest<int>;

//public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
//{
//    public CreateCategoryCommandValidator(SawoodamoDbContext context)
//    {
//        RuleFor(x => x.Name)
//            .MaximumLength(Constants.Product.ProductNameMaxLength)
//                .WithMessage($"Maximum length of the name should be {Constants.Product.ProductNameMaxLength} symbols")
//            .NotNull()
//                .WithMessage("Name is required")
//            .NotEmpty()
//                .WithMessage("Name is required")
//            .MustAsync(async (name, cancellation) =>
//            {
//                return await context.Products.FirstOrDefaultAsync(x => x.Name == name, cancellation) is null;
//            });

//        RuleFor(x => x.Slug)
//            .Matches(new Regex("^[a-z0-9]+(?:-[a-z0-9]+)*$"))
//                .WithMessage("Invalid slug")
//            .MaximumLength(Constants.Product.ProductSlugMaxLength)
//                .WithMessage($"Maximum length of the slug should be {Constants.Product.ProductNameMaxLength} symbols")
//            .MustAsync(async (slug, cancellation) =>
//            {
//                return await context.Products.FirstOrDefaultAsync(x => x.Slug == slug, cancellation) is null;
//            });

//        RuleFor(x => x.Order)
//            .GreaterThan(0)
//                .WithMessage("Invalid order");
//    }
//}

public sealed class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)=>
        app.MapPost("api/category", async (CreateCategoryCommand command, ISender sender) => 
            Results.Ok(await sender.Send(command)))

    .WithTags("Category");
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
