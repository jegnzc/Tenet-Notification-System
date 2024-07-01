using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Infrastructure.Persistence.NotificationChannels;

public class NotificationChannelConfigurations : IEntityTypeConfiguration<NotificationChannel>
{
    public void Configure(EntityTypeBuilder<NotificationChannel> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .ValueGeneratedNever();

        builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasData(
            new NotificationChannel(0, "Email"),
            new NotificationChannel(1, "SMS"),
            new NotificationChannel(2, "Push")
        );
    }
}