using CookingFrog.Domain;
using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Client.Components;

public partial class IngredientAddModal(IJSRuntime JsRuntime)
{
    private IngredientAddModel _newIngredient = IngredientAddModel.Empty;

    [Parameter] public EventCallback<IngredientAddModel> AddClicked { get; set; }

    private void UpdateUnit(UnitEnum unit)
    {
        _newIngredient.Unit = unit;
    }

    private void UpdateQuantity(decimal quantity)
    {
        _newIngredient.Quantity = quantity;
    }
}