namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

//This is not done properly. The reason is that for demo purpose it is not necessary
public record class CreateOrderDto
(
    string ProductName,
    int Amount,
    int? ProductId = null
);