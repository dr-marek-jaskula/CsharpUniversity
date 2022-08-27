namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class OrderDto
(
    int Id,
    int Amount,
    string Status,
    DateTime Deadline,
    OrderProductDto Product,
    PaymentDto Payment
);