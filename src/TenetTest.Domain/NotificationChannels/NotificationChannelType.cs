using Ardalis.SmartEnum;

namespace TenetTest.Domain.NotificationChannels;

public class NotificationChannelType(string name, int value) : SmartEnum<NotificationChannelType>(name, value)
{
    public static readonly NotificationChannelType Email = new(nameof(Email), 0);
    public static readonly NotificationChannelType SMS = new(nameof(SMS), 1);
    public static readonly NotificationChannelType Push = new(nameof(Push), 2);
}