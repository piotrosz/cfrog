using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    private Recipe? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null)
        {
            Recipe = await RecipesRepo.GetRecipe(System.Guid.Parse(Guid));
        }
    }

    [Parameter] public string? Guid { get; set; }
}