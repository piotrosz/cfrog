using Azure;
using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed class RecipesAzReadRepo(TableServiceClient tableServiceClient) : IRecipesReadRepo
{
    public async Task<Recipe> GetRecipe(Guid guid)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = await tableClient.GetEntityAsync<RecipeTableEntity>("_default", guid.ToString());
        return entity.Value.Map();
    }

    public async Task<IReadOnlyList<RecipeSummary>> GetRecipeSummaries()
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var queryResultsFilter = tableClient.QueryAsync<RecipeSummaryTableEntity>(filter: "PartitionKey eq '_default'");
        
        var result = new List<RecipeSummary>();
        
        await foreach (RecipeSummaryTableEntity qEntity in queryResultsFilter)
        {
            result.Add(qEntity.Map());
        }

        return result;
    }
}
