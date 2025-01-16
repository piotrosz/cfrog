using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class IngredientDisplay
{
    private List<string> _styles = ["bg-info", "bg-danger", "bg-warning text-dark", "bg-info text-dark", "bg-success"];

    [Parameter] public required Ingredient Ingredient { get; set; }

    [Parameter] public int GroupIndex { get; set; }
}