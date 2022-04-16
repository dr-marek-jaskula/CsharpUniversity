namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class OrderDto
(
    int Id,
    int Amount,
    string Status,
    DateTime Deadline,
    OrderProductDto Product,
    PaymentDto Payment
);