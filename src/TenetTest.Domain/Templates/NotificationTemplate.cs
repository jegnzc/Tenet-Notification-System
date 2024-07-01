using TenetTest.Domain.Common;
using TenetTest.Domain.NotificationChannels;

using Throw;

namespace TenetTest.Domain.Templates;
public class NotificationTemplate : Entity
{
    public string Name { get; private set; } = null!;
    public int NotificationChannelId { get; private set; }
    public NotificationChannel? NotificationChannel { get; private set; }
    public string? Subject { get; private set; }
    public string Body { get; private set; } = null!;

    private NotificationTemplate() { } // For EF Core

    public NotificationTemplate(string name, NotificationChannelType channelType, string? subject, string body, Guid? id = null) : base(Guid.NewGuid())
    {
        Id = id ?? Guid.NewGuid();
        Name = name.Throw().IfNullOrWhiteSpace(x => x);
        NotificationChannelId = channelType;
        Subject = subject;
        Body = body.Throw().IfNullOrWhiteSpace(x => x);
    }
}