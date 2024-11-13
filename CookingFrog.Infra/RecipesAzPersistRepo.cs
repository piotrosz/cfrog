using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public class RecipesAzPersistRepo(TableServiceClient tableServiceClient) : IRecipesPersistRepo
{
    public async Task Save(Recipe recipe, CancellationToken cancellationToken)
    {
        var recipeEntity = recipe.MapToTableEntity();

        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipesTableName, cancellationToken);
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        await tableClient.AddEntityAsync(recipeEntity, cancellationToken);
    }

    public async Task Save(RecipeSummary recipeSummary, CancellationToken cancellationToken)
    {
        var recipeSummaryEntity = recipeSummary.MapToTableEntity();
        
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipeSummariesTableName, cancellationToken);
        
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        await tableClient.AddEntityAsync(recipeSummaryEntity, cancellationToken);
    }
}