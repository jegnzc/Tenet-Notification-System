namespace TenetTest.Contracts.Notifications;

public record SendNotificationRequest(
    string Recipient,
    string Priority,
    string Channel,
    string? Subject,
    string Body
    );