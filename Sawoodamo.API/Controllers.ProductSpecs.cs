namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder ProductSpec(this RouteGroupBuilder group)
    {
        group.MapPost("", async (CreateProductSpecCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        }).RequireRoles(Constants.Roles.Admin);

        group.MapPut("", async (UpdateProductSpecCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        }).RequireRoles(Constants.Roles.Admin);

        group.MapDelete("{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductSpecCommand(id), cancellationToken);
            return Results.Ok();
        }).RequireRoles(Constants.Roles.Admin);

        return group;
    }
}
