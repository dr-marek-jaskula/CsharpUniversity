using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASPDotNetLearningApplication
{
    public class OrderCountRequirementHandler : AuthorizationHandler<OrderCountRequirement>
    {
        private readonly MyDbContext _context;

        public OrderCountRequirementHandler(MyDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderCountRequirement requirement)
        {
            if (context.User.Identity?.IsAuthenticated is false)
                throw new ForbidException();

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var orderCount = _context.Orders.Count(o => o.CustomerId == userId);

            if (orderCount < requirement.MaximumOrderCount)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}