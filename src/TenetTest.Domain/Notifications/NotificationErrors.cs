using ErrorOr;

namespace TenetTest.Domain.Notifications;

public static class NotificationErrors
{
    public static readonly Error AlreadySent = Error.Conflict(
        code: "Notification.AlreadySent",
        description: "The notification has already been sent.");

    public static readonly Error AlreadyFailed = Error.Conflict(
        code: "Notification.AlreadyFailed",
        description: "The notification has already failed.");
}