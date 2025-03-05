using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Client.Components;

public partial class UnitEdit
{
    [Parameter] 
    public UnitEnum Unit { get; set; }

    private UnitEnum _selectedValue;

    [Parameter] 
    public EventCallback<UnitEnum> UpdateClicked { get; set; }

    private async Task PassValue(UnitEnum value)
    {
        await UpdateClicked.InvokeAsync(value);
        _selectedValue = value;
    }

    private IReadOnlyList<UnitOption> _unitOptions = new List<UnitOption>();

    protected override void OnInitialized()
    {
        FillUnitOptions();
        _selectedValue = Unit;
    }

    private void FillUnitOptions()
    {
        var allUnits = Enum.GetValues<UnitEnum>();

        _unitOptions = allUnits
            .Select(unit => new UnitOption
            {
                Value = unit,
                Description = unit.GetDisplayDescription(1)
            })
            .ToList();
    }

    private class UnitOption
    {
        public UnitEnum Value { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}