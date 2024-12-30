using CookingFrog.Domain;
using CookingFrog.WebUI.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    [Inject]
    public IRecipesReadRepo? RecipesReadRepo { get; set; }
        
    [Inject]
    public IRecipesUpdateRepo? RecipesUpdateRepo { get; set; }
    
    private Recipe? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await RecipesReadRepo!.GetRecipe(guid);
        }
    }

    [Parameter] 
    public string? Guid { get; set; }
    
    private async Task UpdateSummary(string? newSummary)
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(newSummary))
        {
            await RecipesUpdateRepo!.UpdateTitle(newSummary, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task UpdateStep(StepUpdateModel stepModel)
    {
        if (Recipe is not null && stepModel is not null)
        {
            await RecipesUpdateRepo!.UpdateStep(stepModel.StepIndex, stepModel.Description, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }
}