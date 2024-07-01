using ErrorOr;

using MediatR;

namespace TenetTest.Application.Orders.Commands;

public record PlaceOrderCommand(
    string OrderNumber,
    DateTime OrderDate,
    string CustomerEmail,
    string CustomerName,
    decimal TotalAmount) : IRequest<ErrorOr<Success>>;