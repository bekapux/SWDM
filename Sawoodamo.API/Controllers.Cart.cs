namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Cart(this RouteGroupBuilder group)
    {
        group.MapPost("", async (AddProductToCartCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });
        
        group.MapPut("", async (ChangeOrRemoveProductFromCartCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });
        
        group.MapDelete("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new ClearCartCommand(), cancellationToken);
            return Results.Ok();
        });

        return group;
    }
}
