using System.Text.Json.Serialization;

namespace CookingFrog.Domain;

[method: JsonConstructor]
public sealed record Ingredient(string Name, Quantity Quantity, string? GroupName = null)
{
    public Ingredient(string name) : this(name, Quantity.Undefined)
    {
    }
    
    public int? Alternative { get ; set ; }
}