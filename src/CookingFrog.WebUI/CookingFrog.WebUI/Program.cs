using CookingFrog.Infra;
using CookingFrog.WebUI.Components;
using CookingFrog.WebUI;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using CookingFrog.WebUI.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

if (builder.Environment.IsProduction())
{
    var keyVaultUrl = builder.Configuration["Azure:KeyVault:Uri"];

    if (keyVaultUrl == null)
    {
        throw new InvalidOperationException("Azure Key Vault is not configured.");
    }
        
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUrl),
        new DefaultAzureCredential());
}

var azureStorageConfig = builder.Configuration
    .GetSection("Azure:Storage")
    .Get<AzureStorageConfig>();

if (azureStorageConfig == null)
{
    throw new InvalidOperationException("Azure Storage is not configured.");
}

builder.Services.AddFrogStorage(
   azureStorageConfig.Uri,
   azureStorageConfig.AccountName,
   azureStorageConfig.AccountKey);

builder.Services.AddScoped<SearchService>();

AddGoogleAuthentication(builder);
AddAuthorization(builder);

var app = builder.Build();

// -----------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CookingFrog.WebUI.Client._Imports).Assembly);

app.Run();
void AddGoogleAuthentication(WebApplicationBuilder webApplicationBuilder)
{
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
    
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
        .AddCookie(authScheme, options =>
        {
            options.Cookie.Name = ".ap.user";
        })
        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = googleAuthConfig.ClientId;
            options.ClientSecret = googleAuthConfig.Secret;
    
            options.SignInScheme = authScheme;
            options.AccessDeniedPath = "/"; // TODO
            
        })
        .AddIdentityCookies();
}

void AddAuthorization(WebApplicationBuilder webApplicationBuilder)
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



