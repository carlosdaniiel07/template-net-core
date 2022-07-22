using System.Net;
using System.Text.Json;

using TemplateNetCore.Application.Exceptions;

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
            } catch (Exception ex)
            {
                var response = context.Response;
                
                response.ContentType = "application/json";
                response.StatusCode = ex is CustomException ? (ex as CustomException).StatusCode : (int)HttpStatusCode.InternalServerError;

                var message = response.StatusCode == 500 ? "Ocorreu um erro desconhecido. Tente novamente mais tarde" : ex.Message;
                var jsonResponse = JsonSerializer.Serialize(new { message });

                _logger.LogError(ex, message);

                await response.WriteAsync(jsonResponse);
            }
        }
    }
}
