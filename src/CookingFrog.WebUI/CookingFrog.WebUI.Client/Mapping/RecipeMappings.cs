using CookingFrog.Domain;
using CookingFrog.WebUI.Client.Models;

namespace CookingFrog.WebUI.Client.Mapping;

public static class RecipeMappings
{
    public static Ingredient MapToDomain(this IngredientModel ingredientModel)
    {
        return new Ingredient(ingredientModel.Name, 
            new Quantity(ingredientModel.Amount, ingredientModel.Unit),
            ingredientModel.GroupName,
            ingredientModel.Alternative);
    }
    
    public static RecipeModel MapToRecipeModel(this Recipe recipe)
    {
        var ingredients = recipe.Ingredients
            .Select(x => new IngredientModel(x.Name, x.Quantity.Value, x.Quantity.Unit, x.GroupName, x.Alternative));

        var steps = recipe.Steps.Select(x => x.Description);
    
        var recipeModel = new RecipeModel(
            recipe.Summary, 
            recipe.Guid, 
            ingredients,
            steps,
            recipe.Notes); 
        
        return recipeModel;
    }

    public static RecipeSummaryModel MapToRecipeSummaryModel(this RecipeSummary recipeSummary)
    {
        return new RecipeSummaryModel(recipeSummary.Summary, recipeSummary.Guid);
    }
}