using EFCore.Data_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Authentication;

//In order to create a dynamic requirement, we need to inherit from AuthorizationHandler with two generic types. The second one is the one that would be dynamically examined
//Only the manager of the certain shop should be able to update, delete, create shop
//Other can only get access to the read option
public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Order>
{
    private readonly ILogger<ResourceOperationRequirementHandler> _logger;

    public ResourceOperationRequirementHandler(ILogger<ResourceOperationRequirementHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Order order)
    {
        if (requirement.ResourceOperation is ResourceOperation.Read || requirement.ResourceOperation is ResourceOperation.Create)
        {
            _logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.FindFirstValue(ClaimPolicy.PersonId) is string stringPersonId)
        {
            int personId = int.Parse(stringPersonId);

            if (order.CustomerId == personId)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        _logger.LogInformation("Authorization failed");
        return Task.CompletedTask;
    }
}