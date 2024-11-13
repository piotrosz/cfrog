using Azure.Data.Tables;
// using Azure.Identity;
using CookingFrog.Domain;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CookingFrog.Infra;

public static class Registrations
{
    public static void AddFrogStorage(
        this IServiceCollection services, 
        Uri serviceUri,
        string accountName,
        string accountKey)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddTableServiceClient(
                serviceUri,
                new TableSharedKeyCredential(accountName, accountKey));
            //clientBuilder.UseCredential(new DefaultAzureCredential());
        });
        
        services.AddScoped<IRecipesReadRepo, RecipesAzReadRepo>();
        services.AddScoped<IRecipesPersistRepo, RecipesAzPersistRepo>();
        services.AddScoped<IStaticRecipesLoader, StaticRecipesLoader>();
    }
}