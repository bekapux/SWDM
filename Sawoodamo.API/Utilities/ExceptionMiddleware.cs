using Sawoodamo.API.Utilities.Validation;
using System.Net;
using System.Text.Json;

namespace Sawoodamo.API.Utilities;

public sealed class ExceptionMiddleware
{
    #region Constructor

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }

    private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception exception) =>
exception switch
{
    ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors),
    //NotFoundException notFoundException => (HttpStatusCode.NotFound, new[] { new Error("NotFound", notFoundException.Message) }),
    //GeneralException generalException => (HttpStatusCode.BadRequest, new[] { generalException.Error }),
    _ => (HttpStatusCode.InternalServerError, new[] { new Error("General.ServerError", "The server encountered an unrecoverable error.") })
};
}

public class ApiErrorResponse
{
    public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;

    public IReadOnlyCollection<Error> Errors { get; }
}