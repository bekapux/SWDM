namespace Sawoodamo.API.Services;

public interface ISessionService
{
    int GetCurrentUserId();
    string? GetCurrentUserEmail();
}
