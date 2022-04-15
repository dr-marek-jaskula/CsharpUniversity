using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ASP.NETCoreWebAPI.Swagger.SwaggerVersioning;

//Use in specific cases (for other purposes that one here, because this can by achieved by just "options.SubstituteApiVersionInUrl = true;" in "AddVersionedApiExplorer"
public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = new OpenApiPaths();
        foreach (var path in swaggerDoc.Paths)
            paths.Add(path.Key.Replace("{version}", swaggerDoc.Info.Version), path.Value);

        swaggerDoc.Paths = paths;
    }
}