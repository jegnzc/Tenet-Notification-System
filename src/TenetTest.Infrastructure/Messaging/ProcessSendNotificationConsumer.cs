using MassTransit;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;

namespace TenetTest.Infrastructure.Messaging;

public class ProcessSendNotificationConsumer(
    IEmailService _emailService,
    ISmsService _smsService,
    IPushNotificationService _pushNotificationService,
    INotificationsRepository _notificationsRepository) : IConsumer<ISendNotificationMessage>
{
    public async Task Consume(ConsumeContext<ISendNotificationMessage> context)
    {
        var message = context.Message;

        var notification = new Notification(
            message.Recipient,
            message.Priority,
            message.Channel
        );

        notification.AddContent(message.Subject!, message.Body);

        try
        {
            switch (message.Channel.Name)
            {
                case nameof(NotificationChannelType.Email):
                    await _emailService.SendEmailAsync(message.Recipient, notification.Content.Subject!, notification.Content.Body);
                    break;
                case nameof(NotificationChannelType.SMS):
                    await _smsService.SendSmsAsync(message.Recipient, notification.Content.Body);
                    break;
                case nameof(NotificationChannelType.Push):
                    await _pushNotificationService.SendPushNotificationAsync(message.Recipient, notification.Content.Subject!, notification.Content.Body);
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