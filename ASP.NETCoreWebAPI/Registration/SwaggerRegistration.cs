using ASP.NETCoreWebAPI.Swagger.SwaggerVersioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ASP.NETCoreWebAPI.Registration;

public static class SwaggerRegistration
{
    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();

            //Replaces "{version}" in swagger to for example "1.0" or "1.1"
            //It is not necessary because of "options.SubstituteApiVersionInUrl = true;"
            //Can be used for other purposes
            //options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

            //Without versioning we just need:
            //options.SwaggerDoc("v1", new OpenApiInfo { Title = "CsharpUniversity API", Version = "v1" });

            //Add filters from this assembly (look "Swagger" folder), needed for "builder.Services.AddSwaggerExamplesFromAssemblyOf();"
            options.ExampleFilters();

            //// adds any string you like to the request headers - in this case, a correlation id
            //options.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false);
            //// [SwaggerResponseHeader]
            //options.OperationFilter<AddResponseHeadersFilter>();

            //Integrate xml comments (ones with "///" before the method)
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization (additionally add info about claims and requirements)
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            // add Security information to each operation for OAuth2 (on the right they are padlock generated -> open or close)
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            //To tell the Swagger that authorization is needed -> give bearer and token to be authorized in the swagger
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header, //where the "bearer {toke}" will be stored
                Name = "Authorization", //name of the header
                Type = SecuritySchemeType.ApiKey //the type of security
            });
        });

        //We add swagger response/request examples that can be found in "Swagger" folder. We use "Swashbuckle.AspNetCore.Filters"
        //Then we need to Add another configuration into "AddWaggerGen"
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
    }

    public static void ConfigureSwagger(this WebApplication app)
    {
        //Swagger versioning (add version provider)
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            //Build a swagger endpoint for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            //Overwrite swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
            options.InjectStylesheet("/swaggerstyles/SwaggerDark.css");

            //Use this to remove the "/swagger" in the url to hit swagger
            //options.RoutePrefix = string.Empty; //so in this case use just "https://localhost:7240"
        });
    }
}