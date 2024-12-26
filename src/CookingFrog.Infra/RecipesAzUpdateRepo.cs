using Azure.Data.Tables;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.Infra;

internal class RecipesAzUpdateRepo(TableServiceClient tableServiceClient) : IRecipesUpdateRepo
{
    public async Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        await UpdateSummaryInRecipeSummary(newTitle, recipeGuid, cancellationToken);
        await UpdateSummaryInRecipe(newTitle, recipeGuid, cancellationToken);
        
        return Result.Success();
    }

    private async Task UpdateSummaryInRecipeSummary(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var response = tableClient.GetEntity<RecipeSummaryTableEntity>(
            "_default", 
            recipeGuid.ToString(), 
            cancellationToken: cancellationToken);
        
        var entity = response.Value; 
        entity.Summary = newTitle;

        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }
    
    private async Task UpdateSummaryInRecipe(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var response = tableClient.GetEntity<RecipeTableEntity>(
            "_default", 
            recipeGuid.ToString(),
            cancellationToken: cancellationToken);

        var entity = response.Value; 
        entity.Summary = newTitle;

        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
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