using System.Text.Json.Serialization;

namespace CookingFrog.Application.Models;

public record RecipeModel
{
    [method: JsonConstructor]
    public RecipeModel(string Summary,
        Guid Guid,
        IEnumerable<IngredientModel> Ingredients,
        IEnumerable<string> Steps,
        string? Notes,
        string? ImageUrl,
        TimeSpan TimeToPrepare)
    {
        this.Summary = Summary;
        this.Guid = Guid;
        this.Ingredients = Ingredients;
        this.Steps = Steps;
        this.Notes = Notes;
        this.TimeToPrepare = TimeToPrepare;
    }

    public string? ImageUrl { get; set; }
    public string Summary { get; init; }
    public Guid Guid { get; init; }
    public IEnumerable<IngredientModel> Ingredients { get; init; }
    public IEnumerable<string> Steps { get; init; }
    public string? Notes { get; init; }
    public TimeSpan TimeToPrepare { get; init; }
}
    