namespace CookingFrog.Domain.Parsing;

public static class IngredientParser
{
    public static ParseResult<Ingredient> Parse(string ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);
        if (!ingredient.Contains(';'))
        {
            return ParseResult<Ingredient>.Error("Ingredient must contain 'name' and 'amount' divided by semicolon.");
        }
        
        var parts = ingredient.Split(';');

        if (parts.Length != 2)
        {
            return ParseResult<Ingredient>.Error("Ingredient must contain 'name' and 'amount' divided by semicolon.");
        }
        
        var name = parts[0].Trim();
        var quantity = QuantityParser.Parse(parts[1].Trim());
        
        return !quantity.IsSuccess ? 
            ParseResult<Ingredient>.Error(quantity.ErrorDescription!) : 
            ParseResult<Ingredient>.Success(new Ingredient(name, quantity.Result));
    }
}