namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class PaymentDto
(
    int Id,
    decimal Discount,
    decimal Total,
    string Status,
    DateTime Deadline
);