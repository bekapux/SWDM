using System.Security.Claims;

namespace Sawoodamo.API.Services;

public class SessionService(IHttpContextAccessor contextAccessor) : ISessionService
{
    public string? CurrentUserId() =>

        contextAccessor.HttpContext?.Request is null ? default : contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x =>
                string.Equals(x.Type, ClaimTypes.NameIdentifier, StringComparison.InvariantCultureIgnoreCase))
            ?.Value;

    public string? CurrentUserEmail() =>
        contextAccessor.HttpContext?.Request is null ?
            default :
            contextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
}
