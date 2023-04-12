using System.Net;
using System.Text.Json;
using TemplateNetCore.Domain.Exceptions;

namespace TemplateNetCore.Api.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                
                response.ContentType = "application/json";
                response.StatusCode = ex is BaseException ? (ex as BaseException).StatusCode : (int)HttpStatusCode.InternalServerError;

                var message = response.StatusCode == 500 ? "An unknown error has occurred. Try again later" : ex.Message;
                var jsonResponse = JsonSerializer.Serialize(new { message });

                _logger.LogError(ex, message);

                await response.WriteAsync(jsonResponse);
            }
        }
    }
}
