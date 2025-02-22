using System.Text.Json;
using Azure.Data.Tables;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.Infra;

internal class RecipesAzUpdater(TableServiceClient tableServiceClient) : IRecipesUpdater
{
    public async Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        await UpdateSummaryInRecipeSummary(newTitle, recipeGuid, cancellationToken);
        await UpdateSummaryInRecipe(newTitle, recipeGuid, cancellationToken);
        return Result.Success();
    }

    public async Task<Result> UpdateNotes(string notes, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);

        entity.Notes = notes;
        
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
        
        return Result.Success();
    }

    public async Task UpdateIngredient(int index, Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        
        var ingredients = GetIngredients(entity);

        if (ingredients.Count <= index)
        {
            throw new Exception("Ingredient index is out of range.");
        }
        
        ingredients[index] = ingredient;
        
        entity.SerializedIngredients = JsonSerializer.Serialize(ingredients);
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }
    
    public async Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        
        var steps = GetSteps(entity);

        if (steps.Count <= stepIndex)
        {
            throw new Exception($"Step index {stepIndex} is out of range.");
        }
        
        steps[stepIndex] = description;
        entity.SerializedSteps = JsonSerializer.Serialize(steps);
        await Save(cancellationToken, tableClient, entity);
    }
    
    public async Task AddIngredient(Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        var ingredients = GetIngredients(entity);
        ingredients.Add(ingredient);
        entity.SerializedIngredients = JsonSerializer.Serialize(ingredients);
        await Save(cancellationToken, tableClient, entity);
    }
    
    public Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        var ingredients = GetIngredients(entity);
        ingredients.RemoveAt(index);
        entity.SerializedIngredients = JsonSerializer.Serialize(ingredients);
        return Save(cancellationToken, tableClient, entity);
    }

    public Task AddStep(int? index, Step step, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        var steps = GetSteps(entity);
        
        if(index != null && index < steps.Count && index >= 0)
        {
            steps.Insert(index.Value, step); 
        }
        else
        {
            steps.Add(step);
        }
        
        entity.SerializedSteps = JsonSerializer.Serialize(steps);
        return Save(cancellationToken, tableClient, entity);
    }

    public Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        var steps = GetSteps(entity);
        steps.RemoveAt(index);
        entity.SerializedSteps = JsonSerializer.Serialize(steps);
        return Save(cancellationToken, tableClient, entity);
    }

    private static List<Step> GetSteps(RecipeTableEntity entity)
    {
        var steps = JsonSerializer.Deserialize<List<Step>>(entity.SerializedSteps);

        if (steps == null)
        {
            throw new Exception("Cannot deserialize steps.");
        }

        return steps;
    }
    
    private static List<Ingredient> GetIngredients(RecipeTableEntity entity)
    {
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(entity.SerializedIngredients);

        if (ingredients == null)
        {
            throw new Exception("Cannot deserialize ingredients.");
        }
        return ingredients.OrderIngredients();
    }
    
    private static RecipeTableEntity GetRecipe(Guid recipeGuid, CancellationToken cancellationToken, TableClient tableClient)
    {
        var response = tableClient.GetEntity<RecipeTableEntity>(
            "_default", 
            recipeGuid.ToString(),
            cancellationToken: cancellationToken);
        return response.Value;
    }
    
    private static RecipeSummaryTableEntity GetRecipeSummary(Guid recipeGuid, CancellationToken cancellationToken, TableClient tableClient)
    {
        var response = tableClient.GetEntity<RecipeSummaryTableEntity>(
            "_default", 
            recipeGuid.ToString(), 
            cancellationToken: cancellationToken);
        return response.Value;
    }
    
    private async Task UpdateSummaryInRecipeSummary(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipeSummariesTableName);
        var entity = GetRecipeSummary(recipeGuid, cancellationToken, tableClient);
        entity.Summary = newTitle;
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }

    private async Task UpdateSummaryInRecipe(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var tableClient = tableServiceClient.GetTableClient(AzTableNames.RecipesTableName);
        var entity = GetRecipe(recipeGuid, cancellationToken, tableClient);
        entity.Summary = newTitle;
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }
    
    private static async Task Save(CancellationToken cancellationToken, TableClient tableClient, RecipeTableEntity entity)
    {
        await tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Merge, cancellationToken);
    }
}