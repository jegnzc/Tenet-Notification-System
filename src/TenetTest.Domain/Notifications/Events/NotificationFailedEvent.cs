using TenetTest.Domain.Common;

namespace TenetTest.Domain.Notifications.Events;

public record NotificationFailedEvent(Guid NotificationId) : IDomainEvent;
