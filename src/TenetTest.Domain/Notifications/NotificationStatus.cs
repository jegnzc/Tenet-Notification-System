using Ardalis.SmartEnum;

namespace TenetTest.Domain.Notifications;

public class NotificationStatus(string name, int value) : SmartEnum<NotificationStatus>(name, value)
{
    public static readonly NotificationStatus Pending = new(nameof(Pending), 0);
    public static readonly NotificationStatus Sent = new(nameof(Sent), 1);
    public static readonly NotificationStatus Failed = new(nameof(Failed), 2);
}