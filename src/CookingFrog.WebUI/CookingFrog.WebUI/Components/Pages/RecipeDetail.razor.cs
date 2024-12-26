using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    private bool _isSummaryEditMode;

    private string _recipeSummaryForEdit = string.Empty;
    
    private Recipe? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await RecipesReadRepo.GetRecipe(guid);
            _recipeSummaryForEdit = Recipe.Summary;
        }
    }

    [Parameter] 
    public string? Guid { get; set; }

    private void SummaryEdit()
    {
        _isSummaryEditMode = true;
    }

    private async Task UpdateSummary()
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(_recipeSummaryForEdit))
        {
            await RecipesUpdateRepo.UpdateTitle(_recipeSummaryForEdit, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReadRepo.GetRecipe(Recipe.Guid);
        }
        _isSummaryEditMode = false;
    }
}