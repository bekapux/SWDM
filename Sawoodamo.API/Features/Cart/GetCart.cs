namespace Sawoodamo.API.Features.Cart;

public sealed record GetCartQuery : IRequest<CartPageDetailsDTO>;

public sealed class GetCartQueryHandler (SawoodamoDbContext context, ISessionService sessionService): IRequestHandler<GetCartQuery, CartPageDetailsDTO>
{
    public async Task<CartPageDetailsDTO> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var currentUser = sessionService.CurrentUserId();

        var productsInCart = await context.CartItems
            .Where(x => x.UserId == currentUser)
            .Select(x=> new CartItemDTO(x.Product!.Id, x.Product.Name, x.Quantity, x.Product.Price))
            .ToListAsync(cancellationToken);

        if(productsInCart.Any(x=> x.Price <= 0 || x.Quantity <= 0))
            throw new Exception(ErrorMessageGenerator.InternalServerError);
        
        return new CartPageDetailsDTO(productsInCart);
    }
}

public sealed record CartItemDTO(int ProductId, string ProductName, int Quantity, decimal Price);

public sealed record CartPageDetailsDTO(List<CartItemDTO> CartItems)
{
    public decimal Total => CartItems
        .Select(x => x.Price * x.Quantity)
        .Sum();
}