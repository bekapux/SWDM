using System.Security.Claims;

namespace Sawoodamo.API.Services;

public class SessionService(IHttpContextAccessor contextAccessor) : ISessionService
{
    public string? GetCurrentUserId()
    {
        var request = contextAccessor.HttpContext?.Request;

        if (request == null)
        {
            return default;
        }

        var claim = GetClaimBy(ClaimTypes.NameIdentifier);
        return claim;
    }

    public string? GetCurrentUserEmail()
    {
        if (contextAccessor.HttpContext?.Request is null)
            return default;

        var email = contextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        return email;
    }

    private string? GetClaimBy(string key)
    {
        return contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x =>
                string.Equals(x.Type, key, StringComparison.InvariantCultureIgnoreCase))
            ?.Value;
    }
}
