using ErrorOr;

using TenetTest.Domain.Common;
using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications.Events;

using Throw;

namespace TenetTest.Domain.Notifications;

public class Notification : Entity
{
    public DateTime CreatedAt { get; private set; }
    public string Recipient { get; private set; } = null!;
    public int NotificationChannelId { get; private set; }
    public NotificationChannel? NotificationChannel { get; private set; }
    public NotificationContent Content { get; private set; } = null!;
    public NotificationPriority Priority { get; private set; } = null!;
    public NotificationStatus Status { get; private set; } = null!;

    private Notification() { } // For EF Core

    public Notification(string recipient, NotificationPriority priority, NotificationChannelType channelType, Guid? id = null)
        : base(Guid.NewGuid())
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = NotificationStatus.Pending;
        Priority = priority;
        Recipient = recipient.Throw().IfNullOrWhiteSpace(x => x);
        NotificationChannelId = channelType;
    }

    public ErrorOr<Success> AddContent(string subject, string body)
    {
        if (Status != NotificationStatus.Pending)
        {
            return Error.Failure(description: "Cannot add content to a notification that is not in Pending status.");
        }

        Content = new NotificationContent(subject, body);
        return Result.Success;
    }

    public ErrorOr<Success> MarkAsSent()
    {
        if (Status == NotificationStatus.Sent)
        {
            return NotificationErrors.AlreadySent;
        }

        Status = NotificationStatus.Sent;
        _domainEvents.Add(new NotificationSentEvent(Id));
        return Result.Success;
    }

    public ErrorOr<Success> MarkAsFailed()
    {
        if (Status == NotificationStatus.Failed)
        {
            return NotificationErrors.AlreadyFailed;
        }

        Status = NotificationStatus.Failed;
        _domainEvents.Add(new NotificationFailedEvent(Id));
        return Result.Success;
    }
}