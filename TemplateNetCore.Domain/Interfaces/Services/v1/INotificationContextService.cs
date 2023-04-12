using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Domain.Interfaces.Services.v1
{
    public interface INotificationContextService
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        void AddNotification(string code);
        void AddNotification(Notification notification);
    }
}
