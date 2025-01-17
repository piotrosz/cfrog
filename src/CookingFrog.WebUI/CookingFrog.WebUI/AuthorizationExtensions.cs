using CookingFrog.WebUI.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CookingFrog.WebUI;

public static class AuthorizationExtensions 
{
    public static void AddAuthorization(this WebApplicationBuilder webApplicationBuilder)
    {
        // Register the authorization handler
        webApplicationBuilder.Services.AddSingleton<IAuthorizationHandler, SpecificLoginsHandler>();
    
        var emails = webApplicationBuilder.Configuration["Authorization:Emails"];
    
        if (string.IsNullOrWhiteSpace(emails))
        {
            throw new InvalidOperationException("Emails is not configured.");
        }
        
        var emailsArray = emails.Split(',');
        
        // Add authorization policy
        webApplicationBuilder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("SpecificLoginsPolicy", policy =>
                policy.Requirements.Add(new SpecificLoginsRequirement(emailsArray)));
        });
    }
}