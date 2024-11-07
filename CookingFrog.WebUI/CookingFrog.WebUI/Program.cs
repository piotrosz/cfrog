using CookingFrog.Domain;
using CookingFrog.Infra;
using CookingFrog.WebUI.Components;

using Azure.Identity;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    //.AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IRecipesRepo, RecipesStaticTestRepo>();

// TODO: Move to infrastructure
builder.Services.AddAzureClients(clientBuilder =>
{
    // TODO: Move url to config
    clientBuilder.AddTableServiceClient(new Uri("https://stdiwithazuresdk.table.core.windows.net"));
    clientBuilder.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

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

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    //.AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(CookingFrog.WebUI.Client._Imports).Assembly);

app.Run();
