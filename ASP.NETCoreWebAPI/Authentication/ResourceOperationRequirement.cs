using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;

namespace ASP.NETCoreWebAPI.Authentication;

public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperation ResourceOperation { get; }

    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        ResourceOperation = resourceOperation;
    }
}
