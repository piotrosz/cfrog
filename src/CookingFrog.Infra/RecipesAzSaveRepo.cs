using Azure.Data.Tables;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.Infra;

internal class RecipesAzSaveRepo(TableServiceClient tableServiceClient) : IRecipesSaveRepo
{
    public async Task<Result> SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default)
    {
        if (RecipeSummaryExists(recipe.Summary))
        {
            return Result.Failure($"Recipe '{recipe.Summary}' already exists.");
        }

        await SaveRecipeSummaryOnly(new RecipeSummary(recipe.Guid, recipe.Summary), cancellationToken);
        await SaveRecipeOnly(recipe, cancellationToken);
        return Result.Success();
    }

    private async Task SaveRecipeOnly(Recipe recipe, CancellationToken cancellationToken)
    {
        var recipeEntity = recipe.MapToTableEntity();

        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipesTableName, cancellationToken);
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        await tableClient.AddEntityAsync(recipeEntity, cancellationToken);
    }

    private async Task SaveRecipeSummaryOnly(RecipeSummary recipeSummary, CancellationToken cancellationToken)
    {
        var recipeSummaryEntity = recipeSummary.MapToTableEntity();
        
        await tableServiceClient.CreateTableIfNotExistsAsync(AzTableNames.RecipeSummariesTableName, cancellationToken);
        
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        await tableClient.AddEntityAsync(recipeSummaryEntity, cancellationToken);
    }

    private bool RecipeSummaryExists(string summary)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var result = tableClient.Query<RecipeTableEntity>(x => x.Summary == summary);
        return result.Any();
    }
}