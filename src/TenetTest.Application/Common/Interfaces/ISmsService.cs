namespace TenetTest.Application.Common.Interfaces;

public interface ISmsService
{
    Task SendSmsAsync(string toPhoneNumber, string message);
}