using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TenetTest.Domain.Notifications;

namespace TenetTest.Infrastructure.Persistence.Notifications;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .ValueGeneratedNever();

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.Property(n => n.Status)
            .HasConversion(
                status => status.Value,
                value => NotificationStatus.FromValue(value))
            .IsRequired();

        builder.Property(n => n.Priority)
            .HasConversion(
                priority => priority.Value,
                value => NotificationPriority.FromValue(value))
            .IsRequired();

        builder.Property(n => n.Recipient)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(n => n.Content)
               .WithOne(c => c.Notification)
               .HasForeignKey<NotificationContent>(c => c.NotificationId)
               .IsRequired();

        builder.Navigation(n => n.Content).IsRequired();

        builder.HasOne(n => n.NotificationChannel)
            .WithMany(c => c.Notifications)
            .HasForeignKey(n => n.NotificationChannelId)
            .IsRequired();

        builder.HasIndex(n => n.Recipient);
        builder.HasIndex(n => n.CreatedAt);
        builder.HasIndex(n => n.Status);
        builder.HasIndex(n => n.NotificationChannelId);
    }
}