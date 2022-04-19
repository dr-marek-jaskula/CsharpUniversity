using Microsoft.AspNetCore.Authorization;

namespace ASP.NETCoreWebAPI.Authentication;

public class MinimumOrderCountRequirement : IAuthorizationRequirement
{
    public int MinimumOrderCount { get; set; }

    public MinimumOrderCountRequirement(int minimumOrderCount)
    {
        MinimumOrderCount = minimumOrderCount;
    }
}