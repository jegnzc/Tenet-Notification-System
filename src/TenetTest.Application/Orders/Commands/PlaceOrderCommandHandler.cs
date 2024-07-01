using ErrorOr;

using MediatR;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Orders;

namespace TenetTest.Application.Orders.Commands;

public class PlaceOrderCommandHandler(
    IOrdersRepository _ordersRepository) : IRequestHandler<PlaceOrderCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate that the OrderNumber is unique
        var existingOrder = await _ordersRepository.GetByOrderNumberAsync(request.OrderNumber);

        if (existingOrder is not null)
            return Error.Conflict(description: "Order number must be unique");

        var order = new Order(request.OrderNumber, request.OrderDate, request.CustomerEmail, request.CustomerName, request.TotalAmount);

        // Just to showcase the use of domain events
        order.AddOrderPlacedEvent();

        await _ordersRepository.AddAsync(order);

        return Result.Success;
    }
}