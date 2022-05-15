namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class AddressDto
(
    int Id,
    string City,
    string Country,
    string ZipCode,
    string Street,
    int Building,
    int Flat
);