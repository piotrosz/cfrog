using System.Security.Claims;

namespace CookingFrog.WebUI.Authorization;

public static class UserExtensions
{
    public static string GetEmail(this ClaimsPrincipal user)
    {
        user = user ?? throw new ArgumentNullException(nameof(user));
        var email = user.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Email);
        return email?.Value ?? string.Empty;
    }
}