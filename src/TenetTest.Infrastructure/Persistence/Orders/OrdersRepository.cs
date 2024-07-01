using Microsoft.EntityFrameworkCore;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Orders;
using TenetTest.Infrastructure.Common.Persistence;

namespace TenetTest.Infrastructure.Persistence.Orders;

public class OrdersRepository(TenetTestDbContext context) : IOrdersRepository
{
    private readonly TenetTestDbContext _context = context;

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
    {
        return await _context.Orders.SingleOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }
}