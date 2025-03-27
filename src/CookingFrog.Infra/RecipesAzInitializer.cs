using Azure.Data.Tables;
using CookingFrog.Domain;
using Microsoft.Extensions.Logging;

namespace CookingFrog.Infra;

internal sealed class RecipesAzInitializer(
    TableServiceClient tableServiceClient,
    ILogger<RecipesAzInitializer> logger) : IRecipesAzInitializer
{
    public async Task Initialize()
    {
        logger.LogInformation("Initializing Azure Tables for AccountName: {AccountName}", tableServiceClient.AccountName);

        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipeSummariesTableName);
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipesTableName);
    }
}