using Microsoft.AspNetCore.Authorization;

namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Category(this RouteGroupBuilder group)
    {
        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCategoriesQuery(), cancellationToken);
            return Results.Ok(result);
        });

        group.MapGet("{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCategoryQuery(id), cancellationToken);
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateCategoryCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result);
        }).RequireRoles(Constants.Roles.Admin);

        group.MapPut("", async (ISender sender, UpdateCategoryCommand command, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        }).RequireRoles(Constants.Roles.Admin);

        group.MapDelete("{id}", async (string id, ISender sender, CancellationToken token) =>
        {
            await sender.Send(new DeleteCategoryCommand(id), token);
            Results.Ok();
        }).RequireRoles(Constants.Roles.Admin);

        return group;
    }
}