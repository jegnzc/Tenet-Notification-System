using ErrorOr;

using MassTransit;

using MediatR;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Notifications.Commands.SendTemplatedNotification;

public class SendTemplatedNotificationCommandHandler(
    ITemplateService _templateService,
    IPublishEndpoint _publishEndpoint
) : IRequestHandler<SendTemplatedNotificationCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(SendTemplatedNotificationCommand request, CancellationToken cancellationToken)
    {
        // The objective of this call is to get the template content and validate
        // that it's valid and return the error to the user so he can retry instantly,
        // this is not necessary for events because it's assumed that the event is valid
        var templateResult = await _templateService.GetTemplateContentAsync(request.TemplateId, request.Channel, request.Model);

        if (templateResult.IsError)
        {
            return templateResult.Errors;
        }

        var templateContent = templateResult.Value;

        // Validate that the domain model before sending the message to the queue
        var notification = new Notification(
            request.Recipient,
            request.Priority,
            request.Channel
        );

        var addContentResult = notification.AddContent(templateContent.Subject!, templateContent.Body);

        if (addContentResult.IsError)
        {
            return addContentResult.Errors;
        }

        var sendNotificationMessage = new
        {
            Recipient = request.Recipient,
            Subject = templateContent.Subject,
            Body = templateContent.Body,
            Channel = request.Channel,
            Priority = request.Priority,
        };

        await _publishEndpoint.Publish<ISendNotificationMessage>(sendNotificationMessage, cancellationToken);

        return Result.Success;
    }
}