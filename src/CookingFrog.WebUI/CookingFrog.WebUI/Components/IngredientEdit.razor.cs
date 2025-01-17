using CookingFrog.Domain;
using CookingFrog.WebUI.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class IngredientEdit
{
    [Parameter] public bool ShowEditButtons { get; set; }
    
    [Parameter] public Ingredient? Ingredient { get; set; }

    [Parameter] public int Index { get; set; }

    [Parameter] public int GroupIndex { get; set; }

    [Parameter] public EventCallback<IngredientUpdateModel> UpdateClicked { get; set; }

    [Parameter] public EventCallback<int> DeleteClicked { get; set; }

    private IngredientUpdateModel _updatedIngredient = new(-1, string.Empty, 0, UnitEnum.Undefined, string.Empty);

    protected override void OnInitialized()
    {
        if (Ingredient != null && Index >= 0)
        {
            var quantity = Ingredient.Quantity;
            _updatedIngredient = new IngredientUpdateModel(
                Index,
                Ingredient.Name,
                quantity.Value,
                quantity.Unit,
                Ingredient.GroupName);
        }
    }

    private void UpdateQuantity(decimal quantity)
    {
        _updatedIngredient.Quantity = quantity;
    }

    private void UpdateUnit(UnitEnum unit)
    {
        _updatedIngredient.Unit = unit;
    }

    private bool _isEdit;

    private void Edit()
    {
        _isEdit = true;
    }
}