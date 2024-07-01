using TenetTest.Domain.Common;

namespace TenetTest.Domain.Notifications.Events;

public record NotificationSentEvent(Guid NotificationId) : IDomainEvent;
