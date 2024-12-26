namespace CookingFrog.Domain;

public sealed class Recipe
{
    public Recipe(
        Guid guid,
        string summary,
        TimeSpan timeToPrepare,
        IEnumerable<Ingredient> ingredients,
        IEnumerable<Step> steps)
    {
        Guid = guid;
        Summary = summary;
        Steps = steps;
        Ingredients = ingredients;
        TimeToPrepare = timeToPrepare;
    }
    
    public Guid Guid { get; }

    public string Summary { get; }

    public IEnumerable<Step> Steps { get; }

    public IEnumerable<Ingredient> Ingredients { get; }

    public TimeSpan TimeToPrepare { get; }
    
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