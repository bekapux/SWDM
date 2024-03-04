namespace Sawoodamo.API.Features.Cart;

public sealed record AddProductToCartCommand(int ProductId, int Quantity) : IRequest;

#region Validaton

[ValidationOrder(1)]
public sealed class AddProductToCartCommandValidator : AbstractValidator<AddProductToCartCommand>
{
    public AddProductToCartCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(CartItem.Quantity)));

        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(CartItem.ProductId)));
    }
}

[ValidationOrder(2)]
public sealed class AddProductToCartCommandAbstractValidator : AbstractValidator<AddProductToCartCommand>
{
    public AddProductToCartCommandAbstractValidator(SawoodamoDbContext context)
    {
        RuleFor(x => x.ProductId)
            .MustAsync(async (productId, cancellationToken) =>
                await context.Products.AnyAsync(x => x.Id == productId, cancellationToken))
            .WithMessage(ErrorMessageGenerator.Invalid(nameof(Product.Id)))
            .WithErrorCode("InvalidProductId");
    }
}

#endregion

public sealed class AddProductToCartCommandHandler(SawoodamoDbContext context, ISessionService sessionService)
    : IRequestHandler<AddProductToCartCommand>
{
    public async Task Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        var userId = sessionService.CurrentUserId();

        if (userId is null)
            throw new NotFoundException(nameof(User));

        var cartItem = new CartItem()
        {
            UserId = userId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await context.CartItems.AddAsync(cartItem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}