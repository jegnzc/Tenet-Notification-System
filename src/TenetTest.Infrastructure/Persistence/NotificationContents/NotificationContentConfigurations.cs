using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenetTest.Domain.Notifications;

namespace TenetTest.Infrastructure.Persistence.NotificationContents;

public class NotificationContentConfigurations : IEntityTypeConfiguration<NotificationContent>
{
    public void Configure(EntityTypeBuilder<NotificationContent> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Subject)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(c => c.Body)
            .IsRequired();

        builder.HasOne(c => c.Notification)
               .WithOne(n => n.Content)
               .HasForeignKey<NotificationContent>(c => c.NotificationId)
               .IsRequired();
    }
}