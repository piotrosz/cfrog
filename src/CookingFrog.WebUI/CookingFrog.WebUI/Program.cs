using CookingFrog.Infra;
using CookingFrog.WebUI.Components;
using CookingFrog.WebUI;
using Azure.Identity;
using CookingFrog.WebUI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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

var azureStorageConfig = builder.Configuration
    .GetSection("Azure:Storage")
    .Get<AzureStorageConfig>();

if (azureStorageConfig == null)
{
    throw new InvalidOperationException("Azure Storage is not configured.");
}

if (!string.IsNullOrEmpty(azureStorageConfig.ConnectionString))
{
    Console.WriteLine($"Adding Azure Storage with the connection string '{azureStorageConfig.ConnectionString}'.");
    builder.Services.AddFrogStorage(
        azureStorageConfig.ConnectionString);
}
else
{
    Console.WriteLine("Adding Azure Storage with account key.");
    builder.Services.AddFrogStorage(
        azureStorageConfig.Uri,
        azureStorageConfig.AccountName,
        azureStorageConfig.AccountKey);
}

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

app.Run();