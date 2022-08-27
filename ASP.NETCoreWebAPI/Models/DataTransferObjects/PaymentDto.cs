namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class PaymentDto
(
    int Id,
    decimal Discount,
    decimal Total,
    string Status,
    DateTime Deadline
);