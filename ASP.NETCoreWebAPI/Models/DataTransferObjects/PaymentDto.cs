namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class PaymentDto
(
    int Id,
    string Discount,
    decimal Total,
    string Status,
    DateTime Deadline
);