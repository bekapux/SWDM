namespace Sawoodamo.API;

public static partial class Controllers
{
    private static RouteGroupBuilder Auth(this RouteGroupBuilder group)
    {
        group.MapPost("", async (HttpContext httpContext, AuthenticateRequest command, ISender sender, CancellationToken cancellationToken) =>
        {
            var authResponse = await sender.Send(command, cancellationToken);

            if (authResponse is null || string.IsNullOrWhiteSpace(authResponse.Token))
                return Results.Unauthorized();

            httpContext.Response.Cookies.Append("authToken", authResponse.Token, GenerateCookieOptions(true));
            httpContext.Response.Cookies.Append("email", authResponse.Email, GenerateCookieOptions(false));
            httpContext.Response.Cookies.Append("userid", authResponse.UserId, GenerateCookieOptions(false));

            return Results.Ok(new { authResponse.UserId, authResponse.Email });
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
