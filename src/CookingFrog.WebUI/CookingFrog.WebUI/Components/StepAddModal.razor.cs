using CookingFrog.WebUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Components;

public partial class StepAddModal
{
    [Inject] public required IJSRuntime JsRuntime { get; set; }

    private string _step = string.Empty;
    private int? _index = null;

    private void ChangeValue(int? value)
    {
        _index = value;
    }

    [Parameter] public EventCallback<AddStepModel> AddClicked { get; set; }
}