namespace CookingFrog.Domain;

public static class IngredientListExtensions
{
    public static List<Ingredient> OrderIngredients(this IEnumerable<Ingredient> ingredients)
    {
        return ingredients
            .OrderBy(x => x.Name)
            .GroupBy(i => i.GroupName)
            .Select(g => g.ToList())
            .SelectMany(x => x)
            .OrderBy(x => x.Alternative)
            .ToList();
    }
}