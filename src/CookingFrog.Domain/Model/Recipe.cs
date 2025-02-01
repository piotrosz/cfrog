namespace CookingFrog.Domain;

public sealed record Recipe(
    Guid Guid,
    string Summary,
    TimeSpan TimeToPrepare,
    IEnumerable<Ingredient> Ingredients,
    IEnumerable<Step> Steps)
{
    public string Link { get; init; } = string.Empty;
    
    public static Recipe Create(
        Guid guid,
        string title, 
        TimeSpan timeToPrepare,
        IEnumerable<Ingredient> ingredients, 
        IEnumerable<Step> steps)
    {
        return new Recipe(guid, title, timeToPrepare, ingredients, steps);
    }
}