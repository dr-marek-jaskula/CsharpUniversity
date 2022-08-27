namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class UpdateOrderDto
(
    int Amount,
    int? ProductId = null
);