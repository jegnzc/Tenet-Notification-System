using ErrorOr;

using MediatR;

using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Notifications.Commands.SendNotification;

public record SendNotificationCommand(
    string Recipient,
    NotificationPriority Priority,
    NotificationChannelType Channel,
    string? Subject,
    string Body
    ) : IRequest<ErrorOr<Success>>;