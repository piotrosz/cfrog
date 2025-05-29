namespace CookingFrog.Domain;

public sealed record Recipe(
    Guid Guid,
    string Summary,
    TimeSpan TimeToPrepare,
    IEnumerable<Ingredient> Ingredients,
    IEnumerable<Step> Steps,
    string Notes,
    string? ImageUrl = null)
{
    public Recipe(Guid Guid,
        string Summary,
        TimeSpan TimeToPrepare,
        IEnumerable<Ingredient> Ingredients,
        IEnumerable<Step> Steps) : this(Guid, Summary, TimeToPrepare, Ingredients, Steps, string.Empty, ImageUrl: null)
    {
    }
}