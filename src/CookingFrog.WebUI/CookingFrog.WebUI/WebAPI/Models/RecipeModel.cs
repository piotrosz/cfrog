namespace CookingFrog.WebUI.WebAPI.Models;

public record RecipeModel(
    string Summary, 
    Guid Guid, 
    IEnumerable<IngredientModel> Ingredients,
    IEnumerable<string> Steps);