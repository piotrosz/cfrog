using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

// TODO: Add result

internal class RecipesAzPersistRepo(TableServiceClient tableServiceClient) : IRecipesPersistRepo
{
    public async Task SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default)
    {
        if (RecipeExists(recipe.Summary))
        {
            return;
        }

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

    private bool RecipeExists(string summary)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var result = tableClient.Query<RecipeTableEntity>(x => x.Summary == summary);
        return result.Any();
    }
}