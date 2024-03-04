namespace Sawoodamo.API.Features.Cart;

public sealed record ClearCartCommand : IRequest;

public sealed class ClearCartCommandHandler(SawoodamoDbContext context, ISessionService sessionService) : IRequestHandler<ClearCartCommand>
{
    public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var userId = sessionService.CurrentUserId();
        
        await context.CartItems
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}