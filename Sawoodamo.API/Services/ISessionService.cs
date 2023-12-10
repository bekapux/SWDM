namespace Sawoodamo.API.Services;

public interface ISessionService
{
    string? GetCurrentUserId();
    string? GetCurrentUserEmail();
}
