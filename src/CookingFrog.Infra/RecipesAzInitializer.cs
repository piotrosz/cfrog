using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

internal sealed class RecipesAzInitializer(TableServiceClient tableServiceClient) : IRecipesAzInitializer
{
    public async Task Initialize()
    {
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipeSummariesTableName);
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipesTableName);
    }
}