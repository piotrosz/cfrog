using System.Text.Json.Serialization;

namespace CookingFrog.Application.Models;

[method: JsonConstructor]
public record RecipeModel(
    string Summary,
    Guid Guid,
    IEnumerable<IngredientModel> Ingredients,
    IEnumerable<string> Steps,
    string Notes,
    string? ImageUrl,
    TimeSpan TimeToPrepare);
    