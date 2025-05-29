using System.Text.Json;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public static class RecipeToRecipeTableEntityMapper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public static RecipeTableEntity MapToTableEntity(this Recipe recipe)
    {
        return new RecipeTableEntity
        {
            PartitionKey = "_default",
            RowKey = recipe.Guid.ToString(),
            Summary = recipe.Summary,
            TimeToPrepare = recipe.TimeToPrepare,
            ImageUrl = recipe.ImageUrl,
            Notes = recipe.Notes,
            SerializedIngredients = JsonSerializer.Serialize(recipe.Ingredients, Options),
            SerializedSteps = JsonSerializer.Serialize(recipe.Steps, Options)
        };
    }
}