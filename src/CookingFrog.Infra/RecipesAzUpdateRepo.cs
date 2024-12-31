using System.Text.Json;
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

    public async Task UpdateIngredient(int index, Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entityResponse = tableClient.GetEntity<RecipeTableEntity>(
            "_default", 
            recipeGuid.ToString(),
            cancellationToken: cancellationToken);

        var entity = entityResponse.Value;
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(entity.SerializedSteps);

        if (ingredients == null)
        {
            throw new Exception("Cannot deserialize ingredients.");
        }

        if (ingredients.Count <= index)
        {
            throw new Exception("Ingredient index is out of range.");
        }
        
        ingredients[index] = ingredient;
        
        entity.SerializedIngredients = JsonSerializer.Serialize(ingredients);
        
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);}

    public async Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entityResponse = tableClient.GetEntity<RecipeTableEntity>(
            "_default", 
            recipeGuid.ToString(),
            cancellationToken: cancellationToken);

        var entity = entityResponse.Value;
        var steps = JsonSerializer.Deserialize<List<Step>>(entity.SerializedSteps);

        if (steps == null)
        {
            throw new Exception("Cannot deserialize steps.");
        }
        
        if (steps.Count <= stepIndex)
        {
            throw new Exception($"Step index {stepIndex} is out of range.");
        }
        
        steps[stepIndex] = description;
        
        entity.SerializedSteps = JsonSerializer.Serialize(steps);
        
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }
}