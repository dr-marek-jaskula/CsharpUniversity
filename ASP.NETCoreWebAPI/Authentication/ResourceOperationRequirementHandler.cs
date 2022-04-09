using EFCore.Data_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Authentication;

public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Shop>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Shop shop)
    {
        if (requirement.ResourceOperation is ResourceOperation.Read || requirement.ResourceOperation is ResourceOperation.Create)
            context.Succeed(requirement);

        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        if (shop.CreatedById == int.Parse(userId))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
