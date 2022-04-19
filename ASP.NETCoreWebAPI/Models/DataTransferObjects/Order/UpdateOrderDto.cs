namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class UpdateOrderDto
(
    int Amount,
    int? ProductId = null
);