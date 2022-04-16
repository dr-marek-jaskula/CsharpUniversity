namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class OrderProductDto
(
    int Id,
    string Name,
    decimal Price
);