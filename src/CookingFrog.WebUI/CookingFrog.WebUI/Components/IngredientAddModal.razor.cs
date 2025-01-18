﻿using CookingFrog.Domain;
using CookingFrog.WebUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Components;

public partial class IngredientAddModal
{
    [Inject] public required IJSRuntime JsRuntime { get; set; }

    private IngredientAddModel _newIngredient = new(string.Empty, 0, UnitEnum.Undefined);

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