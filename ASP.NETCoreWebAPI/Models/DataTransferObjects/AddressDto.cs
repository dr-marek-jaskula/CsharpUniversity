namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

//It is good practice to seal records we know we would not require inheriting - this will improve performance a bit
public sealed record class AddressDto
(
    int Id,
    string City,
    string Country,
    string ZipCode,
    string Street,
    int Building,
    int Flat
);