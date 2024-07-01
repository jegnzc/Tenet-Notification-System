using MassTransit;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Infrastructure.Messaging;

public class ProcessNotificationTemplateConsumer(
    ITemplateService _templateService,
    INotificationsRepository _notificationsRepository,
    IEmailService _emailService,
    ISmsService _smsService,
    IPushNotificationService _pushNotificationService) : IConsumer<IProcessNotificationTemplateMessage>
{
    public async Task Consume(ConsumeContext<IProcessNotificationTemplateMessage> context)
    {
        var message = context.Message;

        var templateResult = await _templateService.GetTemplateContentAsync(message.TemplateId, message.Channel, message.Model);

        if (templateResult.IsError)
        {
            return;
        }

        var templateContent = templateResult.Value;

        var notification = new Notification(
            message.Recipient,
            message.Priority,
            message.Channel
        );

        var addContentResult = notification.AddContent(templateContent.Subject!, templateContent.Body);

        if (addContentResult.IsError)
        {
            return;
        }

        try
        {
            switch (message.Channel.Name)
            {
                case nameof(NotificationChannelType.Email):
                    await _emailService.SendEmailAsync(message.Recipient, templateContent.Subject!, templateContent.Body);
                    break;
                case nameof(NotificationChannelType.SMS):
                    await _smsService.SendSmsAsync(message.Recipient, templateContent.Body);
                    break;
                case nameof(NotificationChannelType.Push):
                    await _pushNotificationService.SendPushNotificationAsync(message.Recipient, templateContent.Subject!, templateContent.Body);
                    break;
                default:
                    return;
            }

            var markAsSentResult = notification.MarkAsSent();
        }
        catch (Exception)
        {
            notification.MarkAsFailed();
            throw;
        }
        finally
        {
            await _notificationsRepository.AddAsync(notification);
        }
    }
}