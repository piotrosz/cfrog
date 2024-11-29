using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CookingFrog.WebUI.Authorization;

public class SpecificLoginsHandler : AuthorizationHandler<SpecificLoginsRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecificLoginsRequirement requirement)
    {
        if (context.User.Identity is ClaimsIdentity identity)
        {
            var userLogin = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (userLogin != null && requirement.AllowedLogins.Contains(userLogin))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}