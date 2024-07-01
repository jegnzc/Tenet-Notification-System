using ErrorOr;

using MassTransit;

using MediatR;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Notifications.Commands.SendNotification;

public class SendNotificationCommandHandler(
    IPublishEndpoint publishEndpoint, INotificationsRepository _notificationsRepository) : IRequestHandler<SendNotificationCommand, ErrorOr<Success>>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task<ErrorOr<Success>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification(
            request.Recipient,
            request.Priority,
            request.Channel
        );

        var addContentResult = notification.AddContent(request.Subject!, request.Body);

        if (addContentResult.IsError)
        {
            return addContentResult.Errors;
        }

        await _notificationsRepository.AddAsync(notification);

        var sendNotificationMessage = new
        {
            NotificationId = notification.Id,
            Recipient = request.Recipient,
            Subject = request.Subject,
            Body = request.Body,
            Channel = request.Channel,
            Priority = request.Priority,
        };

        await _publishEndpoint.Publish<ISendNotificationMessage>(sendNotificationMessage, cancellationToken);

        return Result.Success;
    }
}