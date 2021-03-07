using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;

using TemplateNetCore.Service.Exceptions;

namespace TemplateNetCore.Api.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

                var jsonResponse = JsonSerializer.Serialize(new { message = ex?.Message ?? "Ocorreu um erro desconhecido. Tente novamente mais tarde" });

                await response.WriteAsync(jsonResponse);
            }
        }
    }
}
