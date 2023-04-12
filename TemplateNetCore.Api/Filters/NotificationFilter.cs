using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Api.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContextService _notificationContextService;

        public NotificationFilter(INotificationContextService notificationContextService)
        {
            _notificationContextService = notificationContextService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var hasNotifications = _notificationContextService.Notifications.Any();

            if (!hasNotifications)
            {
                await next();
                return;
            }

            var response = context.HttpContext.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            var messages = _notificationContextService.Notifications
                .Select(notification => notification.Code)
                .ToArray();

            await response.WriteAsJsonAsync(new { messages });
        }
    }
}
