namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder ProductSpec(this RouteGroupBuilder group)
    {
        group.MapPost("", async (CreateProductSpecCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });

        group.MapPut("", async (UpdateProductSpecCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });

        group.MapDelete("{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductSpecCommand(id), cancellationToken);
            return Results.Ok();
        });

        return group;
    }
}
