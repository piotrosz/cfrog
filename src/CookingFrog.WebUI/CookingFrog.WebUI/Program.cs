using CookingFrog.Infra;
using CookingFrog.WebUI.Components;
using CookingFrog.WebUI;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    //.AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(builder.Configuration["Azure:KeyVault:Uri"]),
        new DefaultAzureCredential());
}

var azureStorageConfig = builder.Configuration
    .GetSection("Azure:Storage")
    .Get<AzureStorageConfig>();

builder.Services.AddFrogStorage(
   azureStorageConfig.Uri,
   azureStorageConfig.AccountName,
   azureStorageConfig.AccountKey);

AddGoogleAuthentication(builder);

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
app.UseAntiforgery();
app.UseAuthentication();
//app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    //.AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(CookingFrog.WebUI.Client._Imports).Assembly);

app.Run();

void AddGoogleAuthentication(WebApplicationBuilder webApplicationBuilder)
{
    var googleAuthConfig = webApplicationBuilder.Configuration
        .GetSection("Authentication:Google")
        .Get<GoogleAuthConfig>();

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
        })
        .AddIdentityCookies();

    //webApplicationBuilder.Services.AddAuthorization();
}



