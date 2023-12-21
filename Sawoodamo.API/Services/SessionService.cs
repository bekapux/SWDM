using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sawoodamo.API.Services;

public class SessionService(IHttpContextAccessor contextAccessor) : ISessionService
{
    public string? CurrentUserId()
    {

        return contextAccessor.HttpContext?.Request is null ? default : contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x =>
                string.Equals(x.Type, JwtRegisteredClaimNames.Jti, StringComparison.InvariantCultureIgnoreCase))
            ?.Value;
    }

    public string? CurrentUserEmail() =>
        contextAccessor.HttpContext?.Request is null ?
            default :
            contextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
}
