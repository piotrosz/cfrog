using Azure.Data.Tables;
using Azure.Identity;
using CookingFrog.Domain;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CookingFrog.Infra;

public static class Registrations
{
    public static void AddFrogStorage(
        this IServiceCollection services, 
        Uri serviceUri)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddTableServiceClient(serviceUri);
            clientBuilder.UseCredential(new DefaultAzureCredential());
        });
        
        services.AddScoped<IRecipesReadRepo, RecipesStaticTestRepo>();
        services.AddScoped<IRecipesPersistRepo, RecipesAzPersistRepo>();
        services.AddScoped<IStaticRecipesLoader, StaticRecipesLoader>();
    }
}