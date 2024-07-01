using TenetTest.Domain.Orders;

namespace TenetTest.Application.Common.Interfaces;

public interface IOrdersRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid orderId);
    Task<Order?> GetByOrderNumberAsync(string orderNumber);
}