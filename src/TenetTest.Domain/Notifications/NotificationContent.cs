namespace TenetTest.Domain.Notifications;

public class NotificationContent
{
    public Guid Id { get; private set; }
    public string? Subject { get; private set; } = null!;
    public string Body { get; private set; } = null!;
    public Guid NotificationId { get; private set; }
    // Navigation property
    public Notification? Notification { get; private set; }

    private NotificationContent() { } // For EF Core

    public NotificationContent(string? subject, string body)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
    }
}