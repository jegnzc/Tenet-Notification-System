using FirebaseAdmin.Messaging;

using TenetTest.Application.Common.Interfaces;

namespace TenetTest.Infrastructure.Services.PushNotificationService;

public class PushNotificationService : IPushNotificationService
{
    public async Task SendPushNotificationAsync(string deviceId, string title, string message)
    {
        var messageToSend = new Message
        {
            Token = deviceId,
            Notification = new Notification
            {
                Title = title,
                Body = message
            }
        };

        await FirebaseMessaging.DefaultInstance.SendAsync(messageToSend);

        await Task.CompletedTask;
    }
}