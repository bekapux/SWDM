using Microsoft.AspNetCore.Authorization;

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
            .WithTags("Cart")
            .RequireAuthorization();
    }
    
    private static RouteHandlerBuilder RequireRoles(this RouteHandlerBuilder builder, params string[] roles)
    {
        return builder.RequireAuthorization(new AuthorizeAttribute { Roles = string.Join(",", roles) });
    }
}