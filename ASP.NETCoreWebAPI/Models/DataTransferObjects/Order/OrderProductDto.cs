namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class OrderProductDto
(
    int Id,
    string Name,
    decimal Price,
    List<TagDto> Tags
);