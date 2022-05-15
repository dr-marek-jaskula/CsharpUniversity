using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Swashbuckle.AspNetCore.Filters;

namespace ASP.NETCoreWebAPI.Swagger.Responses;

public class GetAddressExample : IExamplesProvider<AddressDto>
{
    public AddressDto GetExamples()
    {
        return new AddressDto
        (
            Id: 0,
            City: "name of a city",
            Country: "name of a country",
            ZipCode: "your zip code",
            Street: "street number",
            Building: 0,
            Flat: 0
        );
    }
}