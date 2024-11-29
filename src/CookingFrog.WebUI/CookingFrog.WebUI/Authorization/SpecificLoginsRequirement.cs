namespace CookingFrog.WebUI.Authorization;

using Microsoft.AspNetCore.Authorization;

public class SpecificLoginsRequirement : IAuthorizationRequirement
{
    public IEnumerable<string> AllowedLogins { get; }

    public SpecificLoginsRequirement(IEnumerable<string> allowedLogins)
    {
        AllowedLogins = allowedLogins;
    }
}