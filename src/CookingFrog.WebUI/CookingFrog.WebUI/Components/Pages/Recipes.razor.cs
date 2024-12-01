using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes
{
    [CascadingParameter] public HttpContext? HttpContext { get; set; }
    
    private IReadOnlyList<RecipeSummary>? _recipes;

    protected override async Task OnInitializedAsync()
    {
        if (HttpContext is { User.Identity.IsAuthenticated: false })
        {
        }
        
        _recipes = await RecipesRepo.GetRecipeSummaries();
    }
}