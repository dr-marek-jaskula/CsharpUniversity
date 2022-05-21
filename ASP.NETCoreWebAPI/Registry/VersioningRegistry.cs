using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Registry;

public static class VersioningRegistry
{
    public static void RegisterVersioning(this IServiceCollection services)
    {
        //In versioning the different controllers usually are separate in other subfolders named "V1" and "V2".
        services.AddApiVersioning(options =>
        {
            //options.DefaultApiVersion = new ApiVersion(1, 0); //set default version to 1.0. This is the same as below (ApiVersion.Default == new ApiVersion(1,0))
            options.DefaultApiVersion = ApiVersion.Default;

            //Router fallback to the default version (specified by the DefaultApiVersion setting) in cases where the router is unable to determine the requested API version.
            options.AssumeDefaultVersionWhenUnspecified = true;

            //Response informs about supported versions of api in "api-supported-versions" header and about obsolete version in header "api-deprecated-versions"
            options.ReportApiVersions = true;

            //Api version needs to be specified in requests.

            //API Versioning Strategies - How to pass the Version Information:
            //1. Default approach: by URL Segment
            //options.ApiVersionReader = new UrlSegmentApiVersionReader();

            //2. Query approach: via query -> In this approach, the version is attached to the URL as a query parameter
            //options.ApiVersionReader = new QueryStringApiVersionReader(); // "/api/account/register/?api-version=1.1" which is default option
            //options.ApiVersionReader = new QueryStringApiVersionReader("v"); // "/api/account/register/?v=1.1" other option

            //3. Header approach: via Header -> In this approach we pass our version information via the request headers
            //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //Change header "CustomHeaderVersion" with value "1.1"
            //options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion");

            //4. ContentType approach: via ContentType -> by extending the media types we use in our request headers to pass on the version information
            //options.ApiVersionReader = new MediaTypeApiVersionReader(); // Content-Type: "application/json;v=1.1" which is default option ("Accept" header)
            //options.ApiVersionReader = new MediaTypeApiVersionReader("v"); // the same as above
            //options.ApiVersionReader = new MediaTypeApiVersionReader("version"); // Content-Type: "application/json;version=1.1" other option

            //5. Reading from more then one sources:
            //Our version information can be obtained from various sources instead of sticking to just one common constraint on all times
            //options.ApiVersionReader = ApiVersionReader.Combine(
            //  new UrlSegmentApiVersionReader(),
            //  new HeaderApiVersionReader("api-version"),
            //  new QueryStringApiVersionReader("api-version"),
            //  new MediaTypeApiVersionReader("version"));
        });

        //Versioning + Swagger
        services.AddVersionedApiExplorer(options =>
        {
            //Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            //The specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            //This option is only necessary when versioning by url segment.
            //The SubstitutionFormat can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });
    }
}