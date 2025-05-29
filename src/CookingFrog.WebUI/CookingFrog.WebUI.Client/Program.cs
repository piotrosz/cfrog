using Blazored.LocalStorage;
using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IRecipesReaderService, ClientRecipesReaderService>();
builder.Services.AddScoped<IRecipesUpdaterService, ClientRecipesUpdaterService>();
builder.Services.AddScoped<IImageUploadService, ClientImageUploadService>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
