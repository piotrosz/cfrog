using CookingFrog.Application.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Client.Components;

public partial class StepAddModal(IJSRuntime JsRuntime)
{
    private string _step = string.Empty;
    private int? _index = null;

    private void ChangeValue(int? value)
    {
        _index = value;
    }

    [Parameter] 
    public EventCallback<AddStepModel> AddClicked { get; set; }
}