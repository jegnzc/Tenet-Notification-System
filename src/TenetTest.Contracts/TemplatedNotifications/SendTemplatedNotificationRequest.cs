namespace TenetTest.Contracts.TemplatedNotifications;

public record SendTemplatedNotificationRequest(
    string Recipient,
    string Priority,
    string Channel,
    Guid TemplateId,
    Dictionary<string, string> Model
);