using TenetTest.Domain.Notifications;
using TenetTest.Domain.Templates;

namespace TenetTest.Domain.NotificationChannels;

public class NotificationChannel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<NotificationTemplate>? Templates { get; set; }
    public ICollection<Notification>? Notifications { get; set; }

    public NotificationChannel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    private NotificationChannel() { } // For EF Core
}