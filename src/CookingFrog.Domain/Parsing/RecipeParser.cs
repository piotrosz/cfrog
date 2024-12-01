using System.Globalization;

namespace CookingFrog.Domain.Parsing;

public static class RecipeParser
{
    public static Recipe Parse(
        string timeToPrepare,
        string summary,
        string ingredients,
        string steps)
    {
        ArgumentNullException.ThrowIfNull(timeToPrepare);
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(ingredients);
        ArgumentNullException.ThrowIfNull(steps);
        
        var parsedTimeToPrepare = TimeSpan.ParseExact(timeToPrepare, "g", CultureInfo.CurrentCulture);
        
        var parsedIngredients = new List<Ingredient>();
        var ingredientsArray = ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var ingredient in ingredientsArray)
        {
            var parsedIngredient = IngredientParser.Parse(ingredient);
            parsedIngredients.Add(parsedIngredient.Result);
        }
        
        var stepsArray = steps.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var parsedSteps = stepsArray.Select(x => new Step(x));

        return new Recipe(Guid.NewGuid(), summary, parsedTimeToPrepare, parsedIngredients, parsedSteps);
    }
}