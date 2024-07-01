using MediatR;

using Microsoft.AspNetCore.Mvc;

using TenetTest.Application.Notifications.Commands.SendNotification;
using TenetTest.Application.Notifications.Commands.SendTemplatedNotification;
using TenetTest.Contracts.Notifications;
using TenetTest.Contracts.TemplatedNotifications;
using TenetTest.Domain.NotificationChannels;

using DomainNotificationPriority = TenetTest.Domain.Notifications.NotificationPriority;

namespace TenetTest.Api.Controllers;

[Route("api/notifications")]
public class NotificationsController(ISender _mediator) : ApiController
{
    [HttpPost("send")]
    public async Task<IActionResult> SendNotification(SendNotificationRequest request)
    {
        if (!NotificationChannelType.TryFromName(
            request.Channel,
            out var notificationType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid notification channel");
        }

        if (!DomainNotificationPriority.TryFromName(
            request.Priority,
            out var priority))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid notification priority");
        }

        var command = new SendNotificationCommand(
            request.Recipient,
            priority,
            notificationType,
            request.Subject,
            request.Body
            );

        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(),
            Problem);
    }

    [HttpPost("send-templated")]
    public async Task<IActionResult> SendTemplatedNotification(SendTemplatedNotificationRequest request)
    {
        if (!NotificationChannelType.TryFromName(
            request.Channel,
            out var notificationType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid notification channel");
        }

        if (!DomainNotificationPriority.TryFromName(
            request.Priority,
            out var priority))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid notification priority");
        }

        var command = new SendTemplatedNotificationCommand(
            request.Recipient,
            priority,
            notificationType,
            request.TemplateId,
            request.Model
        );

        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(),
            Problem);
    }
}