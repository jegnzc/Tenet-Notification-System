namespace TenetTest.Contracts.Orders;

public record PlaceOrderRequest(
    string OrderNumber,
    DateTime OrderDate,
    string CustomerEmail,
    string CustomerName,
    decimal TotalAmount);
