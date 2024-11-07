using CookingFrog.Domain;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes
{
    private IReadOnlyList<RecipeSummary>? recipes;

    protected override async Task OnInitializedAsync()
    {
        recipes = await RecipesRepo.GetRecipes();
    }
}