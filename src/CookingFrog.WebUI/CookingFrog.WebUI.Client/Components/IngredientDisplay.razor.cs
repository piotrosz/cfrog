using CookingFrog.Domain;
using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Client.Components;

public partial class IngredientDisplay
{
    private List<string> _styles = ["bg-info", "bg-danger", "bg-warning text-dark", "bg-info text-dark", "bg-success"];

    [Parameter]
    public bool IsCompleted { get; set; }
    
    [Parameter] public required IngredientModel Ingredient { get; set; }

    [Parameter] public int GroupIndex { get; set; }
    
    private Quantity GetQuantity(IngredientModel ingredient) => new(ingredient.Amount, ingredient.Unit);
}