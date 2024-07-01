using Ardalis.SmartEnum;

namespace TenetTest.Domain.Notifications;

public class NotificationPriority(string name, int value) : SmartEnum<NotificationPriority>(name, value)
{
    public static readonly NotificationPriority High = new(nameof(High), 0);
    public static readonly NotificationPriority Medium = new(nameof(Medium), 1);
    public static readonly NotificationPriority Low = new(nameof(Low), 2);
}