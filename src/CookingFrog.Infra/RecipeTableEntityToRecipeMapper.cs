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
            OrderIngredients(deserializedIngredients),
            deserializedSteps,
            recipe.Notes ?? string.Empty);
        
        return result;
    }

    private static IEnumerable<Ingredient> OrderIngredients(IEnumerable<Ingredient> ingredients)
    {
        return ingredients
            .OrderBy(x => x.Name)
            .GroupBy(x => x.GroupName)
            .Select(x => x.ToList())
            .SelectMany(x => x);
    }
}