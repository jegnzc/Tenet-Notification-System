using System.Text.Json.Serialization;

namespace TenetTest.Contracts.Notifications;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationStatus
{
    Pending,
    Sent,
    Failed
}