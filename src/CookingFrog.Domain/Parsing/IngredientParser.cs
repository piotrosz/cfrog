namespace CookingFrog.Domain.Parsing;

public static class IngredientParser
{
    public static ParseResult<Ingredient> Parse(string ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);
        if (!ingredient.Contains(';'))
        {
            return ParseResult<Ingredient>.Success(new Ingredient(ingredient, Quantity.Undefined));
        }
        
        var parts = ingredient.Trim().Split(';');

        if (parts.Length != 2)
        {
            return ParseResult<Ingredient>.Error("Ingredient must contain 'name' and 'amount' divided by semicolon.");
        }
        
        var name = parts[1].Trim();
        var quantity = QuantityParser.Parse(parts[0].Trim());
        
        return !quantity.IsSuccess ? 
            ParseResult<Ingredient>.Error(quantity.ErrorDescription!) : 
            ParseResult<Ingredient>.Success(new Ingredient(name, quantity.Result));
    }
}