using ErrorOr;

using TenetTest.Domain.Common;
using TenetTest.Domain.Orders.Events;

using Throw;

namespace TenetTest.Domain.Orders;

public class Order : Entity
{
    public string OrderNumber { get; private set; } = null!;
    public DateTime OrderDate { get; private set; }
    public string CustomerEmail { get; private set; } = null!;
    public string CustomerName { get; private set; } = null!;
    public decimal TotalAmount { get; private set; }

    private Order() { } // For EF Core

    public Order(string orderNumber, DateTime orderDate, string customerEmail, string customerName, decimal totalAmount, Guid? id = null)
        : base(Guid.NewGuid())
    {
        Id = id ?? Guid.NewGuid();
        OrderNumber = orderNumber.Throw().IfNullOrWhiteSpace(x => x);
        OrderDate = orderDate;
        CustomerEmail = customerEmail.Throw().IfNullOrWhiteSpace(x => x);
        CustomerName = customerName.Throw().IfNullOrWhiteSpace(x => x);
        TotalAmount = totalAmount;
    }

    public ErrorOr<Success> AddOrderPlacedEvent()
    {
        _domainEvents.Add(new OrderPlacedEvent(CustomerEmail, CustomerName, OrderNumber));

        return Result.Success;
    }
}