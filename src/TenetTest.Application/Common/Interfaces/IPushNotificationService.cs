namespace TenetTest.Application.Common.Interfaces;

public interface IPushNotificationService
{
    Task SendPushNotificationAsync(string deviceId, string subject, string body);
}