using CookingFrog.Infra;
using CookingFrog.WebUI.Components;
using CookingFrog.WebUI;
using Azure.Identity;
using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.AddScoped<IRecipesReaderService, ServerRecipesReaderService>();

builder.AddGoogleAuthentication();
builder.AddAuthorization();

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

// Minimal API

app.MapGet("/api/summaries", async ([FromQuery] string searchTerm, IRecipesReader reader) =>
{
    var result = string.IsNullOrWhiteSpace(searchTerm)
        ? await reader.GetRecipeSummaries()
        : await reader.QueryRecipeSummaries(searchTerm);
    return result.Select(x => new RecipeSummaryModel(x.Summary, x.Guid));
});

app.MapGet("/api/summaries/{guid}", async (Guid guid, IRecipesReader reader) =>
{
    var recipe = await reader.GetRecipe(guid);
    
    return new RecipeModel(
        recipe.Summary, 
        recipe.Guid, 
        recipe.Ingredients.Select(x => new IngredientModel(x.Name, x.Quantity.Value, x.Quantity.Unit, x.GroupName)),
        recipe.Steps.Select(x => x.Description));

});

app.Run();


