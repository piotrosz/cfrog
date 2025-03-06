using CookingFrog.Application.Models;
using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Client.Components;

public partial class IngredientEdit
{
    [Parameter] 
    public bool ShowEditButtons { get; set; }
    
    [Parameter] 
    public IngredientModel? Ingredient { get; set; }

    [Parameter] 
    public int Index { get; set; }

    [Parameter] 
    public int GroupIndex { get; set; }

    [Parameter] 
    public EventCallback<IngredientUpdateModel> UpdateClicked { get; set; }

    [Parameter] public EventCallback<int> DeleteClicked { get; set; }

    private IngredientUpdateModel _updatedIngredient = new(-1, string.Empty, 0, UnitEnum.Undefined, string.Empty);

    protected override void OnInitialized()
    {
        SetIngredient();
    }

    protected override void OnParametersSet()
    {
        SetIngredient();
    }
    
    private void SetIngredient()
    {
        if (Ingredient != null && Index >= 0)
        {
            _updatedIngredient = new IngredientUpdateModel(
                Index,
                Ingredient.Name,
                Ingredient.Amount,
                Ingredient.Unit,
                Ingredient.GroupName);
            
            _updatedIngredient.Alternative = Ingredient.Alternative;
        }
    }

    private void ChangeAlternativeValue(int? value)
    {
        _updatedIngredient.Alternative = value;
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
    private bool _isCompleted;

    private void Edit()
    {
        _isEdit = true;
    }
}