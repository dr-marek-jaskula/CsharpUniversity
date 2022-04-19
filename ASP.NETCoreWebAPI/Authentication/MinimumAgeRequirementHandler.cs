using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Authentication;

public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    private readonly ILogger<MinimumAgeRequirementHandler> _logger;

    public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        if (context.User.FindFirstValue(ClaimPolicy.DateOfBirth) is string stringDateOfTime)
        {
            DateTime dateOfBirth = DateTime.Parse(stringDateOfTime);

            var username = context.User.FindFirstValue(ClaimTypes.Name);

            _logger.LogInformation($"User: {username} with date of birth: {dateOfBirth} - ");

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
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