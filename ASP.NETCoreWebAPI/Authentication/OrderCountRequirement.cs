using Microsoft.AspNetCore.Authorization;

namespace ASP.NETCoreWebAPI.Authentication;

public class OrderCountRequirement : IAuthorizationRequirement
{
    public int MaximumOrderCount { get; set; }

    public OrderCountRequirement(int maximumOrderCount)
    {
        MaximumOrderCount = maximumOrderCount;
    }
}