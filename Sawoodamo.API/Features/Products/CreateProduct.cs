namespace Sawoodamo.API.Features.Products;

public sealed record CreateProductCommand(
    string Name,
    string ShortDescription,
    string FullDescription,
    string Slug,
    int? Order,
    decimal Price,
    List<CreateProductSpecDTO> ProductSpecDTOs,
    List<string> ProductCategoryIds
) : IRequest<string>;

#region Validators

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Order)
            .Must(order => order is null || order > 0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(Product.Order)));

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(Product.Name)))
            .MaximumLength(Constants.Product.NameMaxLength)
                .WithMessage(ErrorMessageGenerator.Length(nameof(Product.Name), Constants.Product.NameMinLength, Constants.Product.NameMaxLength));
    }
}

#endregion


public class CreateProductCommandHandler(SawoodamoDbContext context) : IRequestHandler<CreateProductCommand, string>
{
    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product
            {
                Name = request.Name,
                FullDescription = request.FullDescription,
                ShortDescription = request.ShortDescription,
                Slug = request.Slug,
                Order = request.Order,
                OriginalPrice = request.Price,
                CurrentPrice = request.Price,
                ProductSpecs = request.ProductSpecDTOs
                .Select(x => new ProductSpec
                {
                    SpecName = x.SpecName,
                    SpecValue = x.SpecValue
                })
                .ToList()
            };


            await context.Products.AddAsync(product, cancellationToken);

            await context.Database.BeginTransactionAsync();

            await context.SaveChangesAsync();

            var categories = await context.Categories
                .Where(c => request.ProductCategoryIds.Contains(c.Id))
                .ToListAsync();

            if (categories.Count == 0)
                return product.Id;

            foreach (var item in categories)
            {
                await context.ProductCategories.AddAsync(new ProductCategory { CategoryId = item.Id, ProductId = product.Id });
            }

            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();

            return product.Id;
        }
        catch (Exception)
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}

public sealed record CreateProductSpecDTO(string SpecName, string SpecValue);