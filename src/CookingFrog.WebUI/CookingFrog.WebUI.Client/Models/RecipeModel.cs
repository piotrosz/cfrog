using System.Text.Json.Serialization;

namespace CookingFrog.WebUI.Client.Models;

public record RecipeModel
{
    public RecipeModel(string Summary,
        Guid Guid,
        IEnumerable<IngredientModel> Ingredients,
        IEnumerable<string> Steps)
    {
        this.Summary = Summary;
        this.Guid = Guid;
        this.Ingredients = Ingredients;
        this.Steps = Steps;
    }
    
    public IEnumerable<IGrouping<string?, IngredientModel>> IngredientsGrouped => Ingredients.GroupBy(x => x.GroupName);
    public string Summary { get; init; }
    public Guid Guid { get; init; }
    
    [JsonIgnore]
    public IEnumerable<IngredientModel> Ingredients { get; init; }
    
    public IEnumerable<string> Steps { get; init; }
}