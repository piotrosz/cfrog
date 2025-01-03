namespace CookingFrog.Domain;

public sealed class Recipe(
    Guid guid,
    string summary,
    TimeSpan timeToPrepare,
    IEnumerable<Ingredient> ingredients,
    IEnumerable<Step> steps)
{
    public Guid Guid { get; } = guid;

    public string Summary { get; } = summary;

    public IEnumerable<Step> Steps { get; } = steps;

    public IEnumerable<Ingredient> Ingredients { get; } = ingredients;

    public TimeSpan TimeToPrepare { get; } = timeToPrepare;

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