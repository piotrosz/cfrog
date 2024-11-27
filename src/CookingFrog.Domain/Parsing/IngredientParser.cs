namespace CookingFrog.Domain.Parsing;

public static class IngredientParser
{
    public static Ingredient Parse(string ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);
        if (!ingredient.Contains(";"))
        {
            // TODO: return error result
        }
        
        var parts = ingredient.Split(';');

        if (parts.Length != 2)
        {
            // TODO: return error result
        }
        
        var name = parts[0].Trim();
        var quantity = QuantityParser.Parse(parts[1].Trim());
        
        return new Ingredient(name, quantity);
    }
}