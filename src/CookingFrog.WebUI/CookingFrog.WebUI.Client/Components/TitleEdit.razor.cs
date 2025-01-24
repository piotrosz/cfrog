using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Client.Components;

public partial class TitleEdit
{
    [Parameter] public string Title { get; set; } = string.Empty;

    [Parameter] public EventCallback<string> UpdateTitleClicked { get; set; }

    private bool _isSummaryEditMode;
    private string _recipeSummaryForEdit = string.Empty;

    protected override void OnInitialized()
    {
        _recipeSummaryForEdit = Title;
    }

    private void SummaryEdit()
    {
        _isSummaryEditMode = true;
    }
}