using Sawoodamo.API.Features.Products;

namespace Sawoodamo.API;

public static class Controllers
{
    public static void UseMinimalControllers(this WebApplication app)
    {
        app.MapGroup("api/category")
            .MapCategoryActions()
            .WithTags("Category");
        
        app.MapGroup("api/product")
            .MapProductActions()
            .WithTags("Product");
    }

    private static RouteGroupBuilder MapCategoryActions(this RouteGroupBuilder group)
    {
        group.MapGet("", async (ISender sender, CancellationToken token) =>
        {
            var result = await sender.Send(new GetCategoriesQuery(), token);
            return Results.Ok(result);
        });

        group.MapGet("{id:int}", async (int id, ISender sender, CancellationToken token) =>
        {
            var result = await sender.Send(new GetCategoryQuery(id), token);
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateCategoryCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(result);
        });

        group.MapPut("", async (ISender sender, UpdateCategoryCommand command, CancellationToken token) =>
        {
            await sender.Send(command, token);
            return Results.Ok();
        });

        group.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            await sender.Send(new DeleteCategoryCommand(id));
            Results.Ok();
        });

        return group;
    }

    private static RouteGroupBuilder MapProductActions(this RouteGroupBuilder group)
    {
        group.MapGet("{slug}", async (ISender sender, string slug, CancellationToken token) =>
        {
            var result = await sender.Send(new GetProductBySlugQuery(slug), token);
            return Results.Ok(result);
        });

        group.MapPost("by-category", async (GetProductsByCategorySlugQuery query, ISender sender, CancellationToken token) =>
        {
            var result = await sender.Send(query, token);
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateProductCommand command, ISender sender) =>
        {
            var result = Results.Ok(await sender.Send(command));
            return Results.Ok(result);
        });

        group.MapPut("", async (UpdateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        group.MapDelete("{id:int}", async (ISender sender, int id, CancellationToken token) =>
        {
            await sender.Send(new DeleteProductCommand(id), token);
            return Results.Ok();
        });

        return group;
    }
}
