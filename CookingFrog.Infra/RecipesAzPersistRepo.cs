using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public class RecipesAzPersistRepo(TableServiceClient tableServiceClient) : IRecipesPersistRepo
{
    private const string RecipesTableName = "recipes";
    private const string RecipeSummariesTableName = "recipesSummaries";
    
    public async Task Save(Recipe recipe, CancellationToken cancellationToken)
    {
        var recipeEntity = recipe.Map();

        await tableServiceClient.CreateTableIfNotExistsAsync(RecipesTableName, cancellationToken);
        var tableClient = tableServiceClient.GetTableClient(RecipesTableName);
        await tableClient.AddEntityAsync(recipeEntity, cancellationToken);
    }

    public async Task Save(RecipeSummary recipeSummary, CancellationToken cancellationToken)
    {
        var recipeSummaryEntity = recipeSummary.Map();
        
        await tableServiceClient.CreateTableIfNotExistsAsync(RecipesTableName, cancellationToken);
        var tableClient = tableServiceClient.GetTableClient(RecipesTableName);
        await tableClient.AddEntityAsync(recipeSummaryEntity, cancellationToken);
    }
}