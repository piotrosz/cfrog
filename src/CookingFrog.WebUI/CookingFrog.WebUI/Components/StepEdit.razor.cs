using CookingFrog.WebUI.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class StepEdit
{
    [Parameter] public bool ShowEditButtons { get; set; } 
    
    [Parameter] public int Index { get; set; }

    [Parameter] public string Description { get; set; } = string.Empty;

    [Parameter] public EventCallback<StepUpdateModel> UpdateClicked { get; set; }

    [Parameter] public EventCallback<int> DeleteClicked { get; set; }

    private bool _isEdit;
    private string _editedStep = string.Empty;

    protected override void OnInitialized()
    {
        _editedStep = Description;
    }

    private void Edit()
    {
        _isEdit = true;
    }
}