using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ASP.NETCoreWebAPI.Swagger.SwaggerVersioning;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        //Add a swagger document for each discovered API version
        //You might choose to skip or document deprecated API versions differently
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "CsharpUniversity API",
            Version = description.ApiVersion.ToString(),

            //Other options:
            Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
            Contact = new OpenApiContact() { Name = "Marek Jaskula", Email = "marek.jaskula1@gmail.com" },
            License = new OpenApiLicense() { Name = "Licese MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}