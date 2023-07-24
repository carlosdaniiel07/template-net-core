using Microsoft.Extensions.Logging;
using System.ComponentModel;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.UseCases
{
    public abstract class BaseUseCase<TUseCase>
    {
        protected readonly ILogger<TUseCase> _logger;
        protected readonly INotificationContextService _notificationContextService;

        protected BaseUseCase(ILogger<TUseCase> logger, INotificationContextService notificationContextService)
        {
            _logger = logger;
            _notificationContextService = notificationContextService;
        }

        protected void AddNotification(string code) =>
            _notificationContextService.AddNotification(code);

        protected void AddNotification(Enum error) =>
            _notificationContextService.AddNotification(GetErrorCode(error));

        protected void AddNotification(Notification notification) =>
            _notificationContextService.AddNotification(notification);

        private string GetErrorCode(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes?.FirstOrDefault()?.Description ?? value.ToString();
        }
    }
}
