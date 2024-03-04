namespace Sawoodamo.API.Features.Cart;

public sealed record ChangeOrRemoveProductFromCartCommand(string ProductId, int Quantity) : IRequest;

#region Valdiators

public sealed class
    ChangeOrRemoveProductFromCartCommandValidator : AbstractValidator<ChangeOrRemoveProductFromCartCommand>
{
    public ChangeOrRemoveProductFromCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .WithMessage(ErrorMessageGenerator.Invalid(nameof(CartItem.ProductId)));
        
        RuleFor(x => x.Quantity)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage(ErrorMessageGenerator.Invalid(nameof(CartItem.Quantity)));
    }
}

#endregion

public sealed class ChangeOrRemoveProductFromCartCommandHandler(
    SawoodamoDbContext context,
    ISessionService sessionService)
    : IRequestHandler<ChangeOrRemoveProductFromCartCommand>
{
    public async Task Handle(ChangeOrRemoveProductFromCartCommand request, CancellationToken cancellationToken)
    {
        var userId = sessionService.CurrentUserId();

        var query = context.CartItems
            .Where(x =>
                x.ProductId == request.ProductId &&
                x.UserId == userId);

        var affectedRows = request.Quantity is 0
            ? await query
                .ExecuteDeleteAsync(cancellationToken)
            : await query
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(y => y.Quantity, request.Quantity), cancellationToken);

        if (affectedRows is 0)
            throw new Exception(ErrorMessageGenerator.InternalServerError);
    }
}