using CookingFrog.Domain;

namespace CookingFrog.Application.Models;

public record IngredientUpdateModel(
    int Index,
    string Name, 
    decimal Quantity, 
    UnitEnum Unit, 
    string? GroupName)
{
    public int Index { get; set; } = Index;
    
    public decimal Quantity { get; set; } = Quantity;
    
    public string Name { get; set; } = Name;
    
    public UnitEnum Unit { get; set; } = Unit;

    public string? GroupName { get; set; } = GroupName;
    
    public int? Alternative { get; set; }
}