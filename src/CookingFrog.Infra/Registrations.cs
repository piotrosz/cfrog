using Azure.Data.Tables;
using CookingFrog.Domain;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CookingFrog.Infra;

public static class Registrations
{
    public static void AddFrogStorage(
        this IServiceCollection services,
        string? connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddTableServiceClient(connectionString);
            clientBuilder.AddBlobServiceClient(connectionString);
        });

        AddServices(services);
    }

    public static void AddFrogStorage(
        this IServiceCollection services, 
        Uri? serviceUri,
        string? accountName,
        string? accountKey)
    {
        ArgumentNullException.ThrowIfNull(serviceUri);
        ArgumentNullException.ThrowIfNull(accountName);
        ArgumentNullException.ThrowIfNull(accountKey);

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddTableServiceClient(
                serviceUri,
                new TableSharedKeyCredential(accountName, accountKey));

            clientBuilder.AddBlobServiceClient(
                serviceUri,
                new Azure.Storage.StorageSharedKeyCredential(accountName, accountKey));

            //clientBuilder.UseCredential(new DefaultAzureCredential());
        });
        
        AddServices(services);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IRecipesAzInitializer, RecipesAzInitializer>();
        services.AddScoped<IRecipesReader, RecipesAzReader>();
        services.AddScoped<IRecipesSaver, RecipesAzSaver>();
        services.AddScoped<IRecipesUpdater, RecipesAzUpdater>();
        services.AddScoped<IStaticRecipesLoader, StaticRecipesLoader>();
        services.AddScoped<IImageUploader, RecipeImageAzUploader>();
    }
}