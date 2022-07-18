using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Customers.Api.Attributes;

//This attributes allows to bind data in a url to the models in a custom way
//For instance, as was done here, we bind data from path and query 
//In order this to work, the model we pass in, needs to have specified the route and body attributes

public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource { get; } = CompositeBindingSource.Create(
        new[] { BindingSource.Path, BindingSource.Query },
        nameof(FromMultiSourceAttribute));
}

//The example class is here for simplicity. We add also a name
//The problem with this approach is that, the following cannot be a record but a class.
public class UpdateOrderRequest
{
    [FromRoute(Name = "id")] public int Id { get; init; }

    [FromBody] public UpdateOrderDto Order { get; set; } = default!;
}
//Record will not work