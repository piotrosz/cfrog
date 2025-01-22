using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Client.Pages;

public partial class QuantityEdit
{
    [Parameter] public decimal? Quantity { get; set; }

    [Parameter] public EventCallback<decimal> UpdateClicked { get; set; }

    public async Task ChangeValue(decimal value)
    {
        await UpdateClicked.InvokeAsync(value);
        _updatedQuantity = value;
    }

    private decimal _updatedQuantity;

    protected override void OnInitialized()
    {
        if (Quantity != null)
        {
            _updatedQuantity = Quantity.Value;
        }
    }
}