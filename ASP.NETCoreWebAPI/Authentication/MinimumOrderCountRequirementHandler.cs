using ASP.NETCoreWebAPI.Exceptions;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Authentication;

public class MinimumOrderCountRequirementHandler : AuthorizationHandler<MinimumOrderCountRequirement>
{
    private readonly MyDbContext _context;
    private readonly ILogger<MinimumAgeRequirementHandler> _logger;

    public MinimumOrderCountRequirementHandler(MyDbContext context, ILogger<MinimumAgeRequirementHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOrderCountRequirement requirement)
    {
        if (context.User.FindFirstValue(ClaimPolicy.PersonId) is string stringPersonId)
        {
            var personId = int.Parse(stringPersonId);

            var orderCount = _context.Orders.Count(o => o.CustomerId == personId);

            if (orderCount >= requirement.MinimumOrderCount)
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