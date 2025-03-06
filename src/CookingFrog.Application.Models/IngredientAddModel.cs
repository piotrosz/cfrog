using CookingFrog.Domain;

namespace CookingFrog.Application.Models;

public record IngredientAddModel(string Name, decimal Quantity, UnitEnum Unit)
{
    public static IngredientAddModel Empty => new(string.Empty, 0, UnitEnum.Undefined);

    public string Name { get; set; } = Name;
    public decimal Quantity { get; set; } = Quantity;
    public UnitEnum Unit { get; set; } = Unit;
};