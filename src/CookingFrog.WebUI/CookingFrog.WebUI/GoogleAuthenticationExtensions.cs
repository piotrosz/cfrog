using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;

namespace CookingFrog.WebUI;

public static class GoogleAuthenticationExtensions
{
    public static void AddGoogleAuthentication(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddCascadingAuthenticationState();
        webApplicationBuilder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        var googleAuthConfig = webApplicationBuilder.Configuration
            .GetSection("Authentication:Google")
            .Get<GoogleAuthConfig>();

        if (googleAuthConfig == null)
        {
            throw new InvalidOperationException("Google authentication is not configured.");
        }

        const string authScheme = "ap-google-auth";

        webApplicationBuilder.Services
            .AddAuthentication(authScheme)
            .AddCookie(authScheme, options => { options.Cookie.Name = ".ap.user"; })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = googleAuthConfig.ClientId;
                options.ClientSecret = googleAuthConfig.Secret;

                options.SignInScheme = authScheme;
                options.AccessDeniedPath = "/"; // TODO

            })
            .AddIdentityCookies();
    }
}