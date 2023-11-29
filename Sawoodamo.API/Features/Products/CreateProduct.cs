using FluentValidation;

namespace Sawoodamo.API.Features.Products;

public sealed record CreateProductCommand(
    string Name,
    string ShortDescription,
    string FullDescription,
    string Slug,
    int CategoryId
) : IRequest<int>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constants.Product.ProductNameMaxLength);
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
            CategoryId = request.CategoryId,
            DateCreated = DateTime.UtcNow
        };

        await context.Products.AddAsync(product, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        return product.Id;
    }
}

public sealed class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapPost("api/products", async (CreateProductCommand command, ISender sender) =>
            Results.Ok(await sender.Send(command)));
}