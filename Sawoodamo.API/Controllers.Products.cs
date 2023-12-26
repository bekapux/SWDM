namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Product(this RouteGroupBuilder group)
    {
        group.MapGet("pinned", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetPinnedProductsQuery(), cancellationToken);
            return Results.Ok(result);
        });

        group.MapGet("by-slug/{slug}", async (ISender sender, string slug, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetProductBySlugQuery(slug), cancellationToken);
            return Results.Ok(result);
        });

        group.MapPost("by-category", async (GetProductsByCategorySlugQuery query, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateProductCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = Results.Ok(await sender.Send(command, cancellationToken));
            return Results.Ok(result);
        });

        group.MapPut("", async (UpdateProductCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });

        group.MapDelete("{id:int}", async (ISender sender, int id, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductCommand(id), cancellationToken);
            return Results.Ok();
        });

        group.MapPost("upload-image/{id:int}/{order:int}",async (ISender sender, IFormFile file, int id, int order, [FromQuery] bool? main, CancellationToken cancellationToken) =>
        {
            await sender.Send(new CreateProductImageCommand(file, id, order, main), cancellationToken);
            return Results.Ok();
        }).DisableAntiforgery();

        group.MapPost("reorder", async (ISender sender, ReorderProductImagesCommand command, CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.Ok();
        });

        group.MapDelete("delete-image/{id:int}", async (int id, [FromQuery] bool? hardDelete, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductImageCommand(id, hardDelete), cancellationToken);
            return Results.Ok();
        });

        return group;
    }
}
