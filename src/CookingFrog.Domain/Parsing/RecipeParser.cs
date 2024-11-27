using System.Globalization;

namespace CookingFrog.Domain.Parsing;

public static class RecipeParser
{
    public static Recipe Parse(
        string timeToPrepare,
        string summary,
        IEnumerable<string> ingredients,
        IEnumerable<string> steps)
    {
        ArgumentNullException.ThrowIfNull(timeToPrepare);
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(ingredients);
        ArgumentNullException.ThrowIfNull(steps);
        
        var parsedTimeToPrepare = TimeSpan.ParseExact(timeToPrepare, "g", CultureInfo.CurrentCulture);
        
        var parsedIngredients = new List<Ingredient>();
        
        foreach (var ingredient in ingredients)
        {
            var parsedIngredient = IngredientParser.Parse(ingredient);
            parsedIngredients.Add(parsedIngredient);
        }
        
        var parsedSteps = steps.Select(x => new Step(x));

        return new Recipe(Guid.NewGuid(), summary, parsedTimeToPrepare, parsedIngredients, parsedSteps);
    }
}