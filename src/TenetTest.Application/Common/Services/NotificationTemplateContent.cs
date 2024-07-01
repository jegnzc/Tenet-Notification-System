namespace TenetTest.Application.Common.Services;

public class NotificationTemplateContent
{
    public string? Subject { get; }
    public string Body { get; }

    public NotificationTemplateContent(string? subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}
