using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Exceptions;

namespace TemplateNetCore.Api.Middlewares;

internal sealed class GlobalErrorHandlerMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = exception switch
            {
                BaseException => (exception as BaseException).StatusCode,
                _ => StatusCodes.Status500InternalServerError,
            },
            Type = exception.GetType().Name,
            Title = "An unknown error has occurred. Try again later",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = new Dictionary<string, object>
            {
                { "requestId", httpContext.TraceIdentifier },
            },
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
