using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

// TODO: Add result
// TODO: Check for unique title

internal class RecipesAzPersistRepo(TableServiceClient tableServiceClient) : IRecipesPersistRepo
{
    public async Task SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default)
    {
        await SaveRecipeSummaryOnly(new RecipeSummary(recipe.Guid, recipe.Summary), cancellationToken);
        await SaveRecipeOnly(recipe, cancellationToken);
    }

    public async Task SaveRecipeOnly(Recipe recipe, CancellationToken cancellationToken)
    {
        var recipeEntity = recipe.MapToTableEntity();

        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipesTableName, cancellationToken);
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        await tableClient.AddEntityAsync(recipeEntity, cancellationToken);
    }

    public async Task SaveRecipeSummaryOnly(RecipeSummary recipeSummary, CancellationToken cancellationToken)
    {
        var recipeSummaryEntity = recipeSummary.MapToTableEntity();
        
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipeSummariesTableName, cancellationToken);
        
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        await tableClient.AddEntityAsync(recipeSummaryEntity, cancellationToken);
    }
}