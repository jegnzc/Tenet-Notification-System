using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Common.Interfaces;

public interface ISendNotificationMessage
{
    string Recipient { get; }
    string? Subject { get; }
    string Body { get; }
    NotificationPriority Priority { get; }
    NotificationChannelType Channel { get; }
}