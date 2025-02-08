namespace CookingFrog.WebUI.Authorization;

using Microsoft.AspNetCore.Authorization;

public class SpecificLoginsRequirement(IEnumerable<string> allowedLogins) : IAuthorizationRequirement
{
    public IEnumerable<string> AllowedLogins { get; } = allowedLogins;
}