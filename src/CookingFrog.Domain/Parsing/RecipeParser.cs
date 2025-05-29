using System.Globalization;

namespace CookingFrog.Domain.Parsing;

public static class RecipeParser
{
    public static ParseResult<Recipe> Parse(
        string timeToPrepare,
        string summary,
        string ingredients,
        string steps,
        string? imageUrl,
        string? notes)
    {
        ArgumentNullException.ThrowIfNull(timeToPrepare);
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(ingredients);
        ArgumentNullException.ThrowIfNull(steps);
        
        var parsedTimeToPrepare = TimeSpan.ParseExact(timeToPrepare, "g", CultureInfo.CurrentCulture);
        
        var parsedIngredients = new List<Ingredient>();
        var ingredientsArray = ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        if (ingredientsArray.Length == 0)
        {
            return ParseResult<Recipe>.Error("Recipe must contain at least one ingredient.", "ingredients");
        }

        foreach (var ingredient in ingredientsArray)
        {
            var parsedIngredient = IngredientParser.Parse(ingredient);
            if (!parsedIngredient.IsSuccess)
            {
                return ParseResult<Recipe>.Error(parsedIngredient.ErrorDescription!, "ingredients");
            }
            parsedIngredients.Add(parsedIngredient.Result);
        }
        
        var stepsArray = steps.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        if (stepsArray.Length == 0)
        {
            return ParseResult<Recipe>.Error("Recipe must contain at least one step.", "steps");
        }

        var parsedSteps = stepsArray.Select(x => new Step(x.Trim()));

        var recipe = new Recipe(Guid.NewGuid(), summary, parsedTimeToPrepare, parsedIngredients, parsedSteps, notes, imageUrl);
        return ParseResult<Recipe>.Success(recipe);
    }
}