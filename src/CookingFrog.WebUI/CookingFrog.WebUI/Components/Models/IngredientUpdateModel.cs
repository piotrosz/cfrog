using CookingFrog.Domain;

namespace CookingFrog.WebUI.Components.Models;

public record IngredientUpdateModel(string Name, decimal Quantity, UnitEnum Unit)
{
    public int Index { get; set; }
    
    public decimal Quantity { get; set; } = Quantity;
    
    public string Name { get; set; } = Name;
    
    public UnitEnum Unit { get; set; } = Unit;
}