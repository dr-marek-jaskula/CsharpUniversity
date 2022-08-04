using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ASP.NETCoreWebAPI.Attributes;

//This attributes can decorate the action.
//Decorated actions can have same route but if they have different accept headers, the request will find the way to the action with given accept header 

//To see this in action, go to UniveristyController

[AttributeUsage(AttributeTargets.Method)]
public class AcceptHeaderAttribute : Attribute, IActionConstraint
{
    private readonly string _acceptHeader;
    public int Order { get; set; }

    public AcceptHeaderAttribute(string acceptHeader)
    {
        _acceptHeader = acceptHeader;
    }

    public bool Accept(ActionConstraintContext context)
    {
        return context.RouteContext.HttpContext.Request.Headers["Accept"].Any(x => x.StartsWith(_acceptHeader));
    }
}
