namespace CookingFrog.Domain;

public sealed record Ingredient(string Name, Quantity Quantity, string? GroupName = null)
{
    public Ingredient(string name) : this(name, Quantity.Undefined)
    {
    }
}