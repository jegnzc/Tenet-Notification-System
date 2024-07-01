using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Common.Interfaces;

public interface IProcessNotificationTemplateMessage
{
    Guid TemplateId { get; }
    Dictionary<string, string> Model { get; }
    string Recipient { get; }
    NotificationChannelType Channel { get; }
    NotificationPriority Priority { get; }
}