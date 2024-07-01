using ErrorOr;

using MediatR;

using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Notifications.Commands.SendTemplatedNotification;

public record SendTemplatedNotificationCommand(
    string Recipient,
    NotificationPriority Priority,
    NotificationChannelType Channel,
    Guid TemplateId,
    Dictionary<string, string> Model
) : IRequest<ErrorOr<Success>>;