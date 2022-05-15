using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Swashbuckle.AspNetCore.Filters;

namespace ASP.NETCoreWebAPI.Swagger.Requests;

public class CreateAddressExample : IExamplesProvider<CreateAddressDto>
{
    public CreateAddressDto GetExamples()
    {
        return new CreateAddressDto
        (
            City: "name of a city",
            Country: "name of a country",
            ZipCode: "your zip code",
            Street: "street number",
            Building: 0,
            Flat: 0
        );
    }
}