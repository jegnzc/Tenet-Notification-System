using System.Text.Json.Serialization;

namespace TenetTest.Contracts.NotificationChannels;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationChannel
{
    Email,
    SMS,
    Push
}