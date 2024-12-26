using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    private bool _isSummaryEditMode;

    private string _recipeSummaryForEdit;
    
    private Recipe? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await RecipesRepo.GetRecipe(guid);
            _recipeSummaryForEdit = Recipe.Summary;
        }
    }

    [Parameter] 
    public string? Guid { get; set; }

    private void SummaryEdit()
    {
        _isSummaryEditMode = true;
    }

    private void UpdateSummary()
    {
        _isSummaryEditMode = false;
        // TODO: Save 
    }
}