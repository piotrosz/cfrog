using System.Text.Json;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public static class RecipeToRecipeTableEntityMapper
{
    public static RecipeTableEntity Map(Recipe recipe)
    {
        return new RecipeTableEntity
        {
            PartitionKey = "_default",
            RowKey = recipe.Guid.ToString(),
            Summary = recipe.Summary,
            TimeToPrepare = recipe.TimeToPrepare,
            SerializedIngredients = JsonSerializer.Serialize(recipe.Ingredients),
            SerializedSteps = JsonSerializer.Serialize(recipe.Steps)
        };
    }
}