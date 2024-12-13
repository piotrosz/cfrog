using Azure.Data.Tables;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.Infra;

internal class RecipesAzUpdateRepo(TableServiceClient tableServiceClient) : IRecipesUpdateRepo
{
    public async Task<Result> UpdateTitle(string newTitle, Guid recipeGuid)
    {
        await UpdateSummaryInRecipeSummary(newTitle, recipeGuid);
        await UpdateSummaryInRecipe(newTitle, recipeGuid);
        
        return Result.Success();
    }

    private async Task UpdateSummaryInRecipeSummary(string newTitle, Guid recipeGuid)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var response = tableClient.GetEntity<RecipeSummaryTableEntity>("_default", recipeGuid.ToString());

        var entity = response.Value; 
        entity.Summary = newTitle;

        await tableClient.UpdateEntityAsync(entity, entity.ETag);
    }
    
    private async Task UpdateSummaryInRecipe(string newTitle, Guid recipeGuid)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var response = tableClient.GetEntity<RecipeTableEntity>("_default", recipeGuid.ToString());

        var entity = response.Value; 
        entity.Summary = newTitle;

        await tableClient.UpdateEntityAsync(entity, entity.ETag);
    }

    public async Task UpdateIngredients(IReadOnlyCollection<Ingredient> ingredients, Guid recipeGuid)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSteps(IReadOnlyCollection<Step> steps, Guid recipeGuid)
    {
        throw new NotImplementedException();
    }
}