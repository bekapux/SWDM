namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Auth(this RouteGroupBuilder group)
    {
        group.MapPost("", async (AuthenticateRequest command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = Results.Ok(await sender.Send(command, cancellationToken));
            return Results.Ok(result);
        });

        return group;
    }
}
