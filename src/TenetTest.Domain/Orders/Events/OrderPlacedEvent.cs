using TenetTest.Domain.Common;

namespace TenetTest.Domain.Orders.Events;

public record OrderPlacedEvent(string CustomerEmail, string CustomerName, string OrderNumber) : IDomainEvent;