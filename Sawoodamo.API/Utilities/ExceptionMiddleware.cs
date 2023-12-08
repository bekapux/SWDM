namespace Sawoodamo.API.Utilities;

public static class ExceptionHandling
{
    public static void HandleExceptions(this WebApplication app)
    {
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                (int httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exceptionHandlerPathFeature?.Error);

                context.Response.StatusCode = httpStatusCode;

                string response = JsonSerializer.Serialize(new ApiErrorResponse(errors));

                await context.Response.WriteAsync(response);
            });
        });
    }

    private static (int httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception? exception)
    {
        return exception switch
        {
            Validation.ValidationException validationException => (StatusCodes.Status400BadRequest, validationException.Errors),
            NotFoundException notFoundException => (StatusCodes.Status404NotFound, new[] { new Error("NotFound", notFoundException.Message) }),
            _ => (StatusCodes.Status500InternalServerError, new[] { new Error("General.ServerError", "Something went wrong") })
        };
    }
}

public sealed record ApiErrorResponse(IReadOnlyCollection<Error> Errors) { }