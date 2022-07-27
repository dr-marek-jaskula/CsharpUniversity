//https://www.meziantou.net/versioning-an-asp-net-core-api.htm

//In order to configure swagger to many version of api we need to:
//0. Add Attributes like "[MapToApiVersion("1.0")]", "[MapToApiVersion("1.1")]" to each action if the controller support more then one version (else swagger would make problems)
//note: Deprecated versions in swagger make action crossed by line

//1. Install NuGet package: Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
//2. Add to program.cs
/*
        services.AddVersionedApiExplorer(options =>
        {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. 
                // SubstitutionFormat can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
        });
*/
//3. Add before UseSwaggerUI
//var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//4. Modify SwaggerUI to the following form:
/*
    app.UseSwaggerUI(options =>
    {
        //Build a swagger endpoint for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

        //Overwrite swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
        options.InjectStylesheet("/swaggerstyles/SwaggerDark.css");
    });
*/

//5. Before AddSwaggerGen add:
//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//6. Extend SwaggerGen by:
//builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
//7. Add
/*
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
*/
//8. You can then get the generated swagger files at:
//https://localhost:7240/swagger/v1/swagger.json
//https://localhost:7240/swagger/v1.1/swagger.json

//////////////////////

//To include the xml comments in swagger:
//1. Add to project file:
/*
  <PropertyGroup>
  	<GenerateDocumentationFile>true</GenerateDocumentationFile>
  	<NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>
*/
//2. Add to AddSwaggerGen the following code:
/*
    //Integrate xml comments (ones with "///" before the method)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
*/
//3. Add some xml comments to method like:
/*
    /// <summary>
    /// This is version 1 test method
    /// </summary>
    /// <returns></returns>
    [HttpGet("VersionTest")]
    public ActionResult VersionTestActionV1()
    {
        return Ok("Version 1");
    }
*/