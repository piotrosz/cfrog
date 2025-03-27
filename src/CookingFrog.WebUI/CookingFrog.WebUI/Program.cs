using CookingFrog.Infra;
using CookingFrog.WebUI.Components;
using CookingFrog.WebUI;
using Azure.Identity;
using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CookingFrog.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// On development environment KeyVault is not required
if (!builder.Environment.IsDevelopment())
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

builder.AddFrogStorage();;

builder.Services.AddScoped<IRecipesReaderService, ServerRecipesReaderService>();
builder.Services.AddScoped<IRecipesUpdaterService, ServerRecipesUpdaterService>();

builder.AddFrogGoogleAuthentication();
builder.AddFrogAuthorization();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApi();
}

var app = builder.Build();

// -----------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CookingFrog.WebUI.Client._Imports).Assembly);

app.UseCfrogMinimalApi();

await InitTables(app);

app.Run();

static async Task InitTables(WebApplication app)
{
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0#resolve-a-service-at-app-startup
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var initializer = services.GetRequiredService<IRecipesAzInitializer>();
        Console.WriteLine("Initializing the recipes storage...");
        await initializer.Initialize();
    }
}