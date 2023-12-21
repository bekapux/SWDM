namespace Sawoodamo.API.Services.Abstractions;

public interface ISessionService
{
    string? CurrentUserId();
    string? CurrentUserEmail();
}
