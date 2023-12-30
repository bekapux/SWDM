namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Auth(this RouteGroupBuilder group)
    {
        group.MapPost("",
            async (HttpContext httpContext, AuthenticateRequest command, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var authResponse = await sender.Send(command, cancellationToken);

                if (authResponse is null || string.IsNullOrWhiteSpace(authResponse.Token))
                    return Results.Unauthorized();

                httpContext.Response.Cookies.Append(Constants.Cookies.AuthToken, authResponse.Token,
                    GenerateCookieOptions(true));
                httpContext.Response.Cookies.Append(Constants.Cookies.Email, authResponse.Email,
                    GenerateCookieOptions(false));
                httpContext.Response.Cookies.Append(Constants.Cookies.UserId, authResponse.UserId,
                    GenerateCookieOptions(false));

                return Results.Ok(new { authResponse.UserId, authResponse.Email });
            });

        group.MapPost("logout", (HttpContext httpContext) =>
        {
            httpContext.Response.Cookies.Delete(Constants.Cookies.AuthToken);
            httpContext.Response.Cookies.Delete(Constants.Cookies.Email);
            httpContext.Response.Cookies.Delete(Constants.Cookies.UserId);
            return Results.Ok();
        });

        return group;
    }

    private static CookieOptions GenerateCookieOptions(bool httpOnly) =>
        new()
        {
            HttpOnly = httpOnly,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(20)
        };
}