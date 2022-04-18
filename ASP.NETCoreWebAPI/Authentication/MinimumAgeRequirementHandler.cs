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
        if (context.User.FindFirstValue("DateOfBirth") is string stringDateOfTime)
        {
            DateTime dateOfBirth = DateTime.Parse(stringDateOfTime);

            var username = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {username} with date of birth: {dateOfBirth} - ");

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        //DateTime dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type is "DateOfBirth").Value);
        _logger.LogInformation("Authorization failed");

        return Task.CompletedTask;
    }
}