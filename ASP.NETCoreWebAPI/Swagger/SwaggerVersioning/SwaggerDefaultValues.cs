using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace ASP.NETCoreWebAPI.Swagger.SwaggerVersioning;

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Parameters is null)
            return;

        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/b7cf75e7905050305b115dd96640ddd6e74c7ac9/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/SwaggerGenerator.cs#L383-L387
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach (var contentType in response.Content.Keys)
                if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
                    response.Content.Remove(contentType);
        }

        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            if (parameter.Description is null)
                parameter.Description = description.ModelMetadata?.Description;

            if (parameter.Schema.Default is null && description.DefaultValue is not null)
            {
                if (description.ModelMetadata is not null)
                {
                    // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                    var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }
                else
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
            }

            parameter.Required |= description.IsRequired;
        }
    }
}