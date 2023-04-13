using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Services.v1
{
    public class NotificationContextService : INotificationContextService
    {
        private readonly List<Notification> _notifications;

        public NotificationContextService()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(string code) =>
            _notifications.Add(new Notification(code));

        public void AddNotification(Notification notification) =>
            _notifications.Add(notification);
    }
}
