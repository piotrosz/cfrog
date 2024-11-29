using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes
{
    [CascadingParameter] public HttpContext HttpContext { get; set; }
    
    private IReadOnlyList<RecipeSummary>? recipes;

    protected override async Task OnInitializedAsync()
    {
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            return;
        }
        
        recipes = await RecipesRepo.GetRecipeSummaries();
    }
}