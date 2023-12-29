namespace Sawoodamo.API;

public static partial class Controllers
{
    public static void UseMinimalControllers(this WebApplication app)
    {
        app.MapGroup("api/category")
            .Category()
            .WithTags("Category");

        app.MapGroup("api/product")
            .Product()
            .WithTags("Product");

        app.MapGroup("api/auth")
            .Auth()
            .WithTags("Auth");

        app.MapGroup("api/product-spec")
            .ProductSpec()
            .WithTags("Product Specs");
        
        app.MapGroup("api/cart")
            .Cart()
            .WithTags("Cart");
    }
}