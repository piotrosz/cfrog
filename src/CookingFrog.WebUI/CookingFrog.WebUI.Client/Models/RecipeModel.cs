using System.Text.Json.Serialization;

namespace CookingFrog.WebUI.Client.Models;

[method: JsonConstructor]
public record RecipeModel(
    string Summary,
    Guid Guid,
    IEnumerable<IngredientModel> Ingredients,
    IEnumerable<string> Steps);