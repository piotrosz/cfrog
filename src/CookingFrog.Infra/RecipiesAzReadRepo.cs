using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

internal sealed class RecipesAzReadRepo(TableServiceClient tableServiceClient) : IRecipesReadRepo
{
    public async Task<Recipe> GetRecipe(Guid guid)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = await tableClient.GetEntityAsync<RecipeTableEntity>("_default", guid.ToString());
        return entity.Value.Map();
    }

    public async Task<IReadOnlyList<RecipeSummary>> GetRecipeSummaries()
    {
        return await Query("PartitionKey eq '_default'");
    }

    public async Task<IReadOnlyList<RecipeSummary>> QueryRecipeSummaries(string searchTerm)
    {
        return await Query($"contains(Summary,'{searchTerm}'");
    }

    private async Task<IReadOnlyList<RecipeSummary>> Query(string filter)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);

        var queryResultsFilter = tableClient.QueryAsync<RecipeSummaryTableEntity>(filter: filter);
        
        var result = new List<RecipeSummary>();
        
        await foreach (RecipeSummaryTableEntity qEntity in queryResultsFilter)
        {
            result.Add(qEntity.Map());
        }
      
        return result;
    }
}
