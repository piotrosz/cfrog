using CookingFrog.Domain;

namespace CookingFrog.WebUI.Client.Models;

public record IngredientAddModel(string Name, decimal Quantity, UnitEnum Unit)
{
    public string Name { get; set; } = Name;
    public decimal Quantity { get; set; } = Quantity;
    public UnitEnum Unit { get; set; } = Unit;
};