using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Api.ExceptionHandling;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred while processing {RequestPath}",
            httpContext.Request.Path
        );

        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,

            InvalidOperationException => StatusCodes.Status409Conflict,

            _ => StatusCodes.Status500InternalServerError,
        };

        var title = statusCode switch
        {
            StatusCodes.Status400BadRequest => "Bad request",

            StatusCodes.Status409Conflict => "Invalid operation",

            _ => "An unexpected error occurred",
        };

        var detail =
            statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : exception.Message;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path,
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
