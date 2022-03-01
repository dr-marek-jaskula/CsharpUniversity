using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPDotNetLearningApplication
{
    public class CreatedMultipleRestaurantRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantRequirement>
    {
        private readonly RestaurantDbContext _context;

        public CreatedMultipleRestaurantRequirementHandler(RestaurantDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated is false) throw new ForbidException();

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdRestaurantCounts = _context.Restaurants.Count(r => r.CreatedById == userId);

            if (createdRestaurantCounts >= requirement.MinimumRestaurantCreated)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
