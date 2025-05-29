using System.Text.Json;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public static class RecipeTableEntityToRecipeMapper
{
    public static Recipe Map(this RecipeTableEntity recipe)
    {
        var deserializedIngredients = JsonSerializer.Deserialize<IEnumerable<Ingredient>>(recipe.SerializedIngredients);
        var deserializedSteps = JsonSerializer.Deserialize<IEnumerable<Step>>(recipe.SerializedSteps);
        
        if (deserializedIngredients == null || deserializedSteps == null)
        {
            throw new NullReferenceException("Recipe could not be deserialized.");
        }
        
        var result = new Recipe(
            Guid.Parse(recipe.RowKey), 
            recipe.Summary,
            recipe.TimeToPrepare,
            deserializedIngredients.OrderIngredients(),
            deserializedSteps,
            recipe.Notes ?? string.Empty,
            recipe.ImageUrl);
        
        return result;
    }
}