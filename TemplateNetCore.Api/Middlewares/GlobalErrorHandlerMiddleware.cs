using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using TemplateNetCore.Domain.Exceptions;

namespace TemplateNetCore.Api.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IExceptionHandler
    {
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = httpContext.Response;

            response.ContentType = "application/json";
            response.StatusCode = exception is BaseException ? (exception as BaseException).StatusCode : (int)HttpStatusCode.InternalServerError;

            var message = response.StatusCode == 500 ? "An unknown error has occurred. Try again later" : exception.Message;
            var jsonResponse = JsonSerializer.Serialize(new { message });

            _logger.LogError(exception, message);

            await response.WriteAsync(jsonResponse, cancellationToken);

            return true;
        }
    }
}
