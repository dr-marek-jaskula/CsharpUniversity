using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace ASP.NETCoreWebAPI.Swagger.Responses;

public class ProblemDetailsExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Type = "URI that specifies the error problem",
            Title = "Title of the error",
            Status = 0,
            Detail = "Problem details",
            Instance = "Request uri"
        };
    }
}