namespace ASP.NETCoreWebAPI.Swagger;

public class SwaggerFiltering
{
    //We will specify the example requests, that will be seen in the swagger
    //To obtain this result we create "Requests" and "Responses" folder, and there we add classes like:
    //"CreateAddressExample" that implements interface "IExamplesProvider<CreateAddressDto>"

    //The most important thing is to provide the "ProblemDetailsExample" and remember to apply attribute like: "[ProducesResponseType(typeof(ProblemDetails), 404)]" to actions

    //Remember to add above the controllers the following attribute:
    //[Produces("application/json")]
    //This attribute will change in the swagger that produces responses are in "application/json" format
}