using MediatR;

using Microsoft.AspNetCore.Mvc;

using TenetTest.Application.Orders.Commands;
using TenetTest.Contracts.Orders;

namespace TenetTest.Api.Controllers;

[Route("api/orders")]
public class OrdersController(ISender _mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest request)
    {
        var command = new PlaceOrderCommand(
            request.OrderNumber,
            request.OrderDate,
            request.CustomerEmail,
            request.CustomerName,
            request.TotalAmount
        );

        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(),
            Problem);
    }
}