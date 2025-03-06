using CookingFrog.Application.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class RecipeList
{
    [Parameter] 
    public required IQueryable<RecipeSummaryModel> Recipes { get; set; }

    [Parameter] 
    public EventCallback<string> OnSearch { get; set; }

    private async Task SearchRecipes(string searchTerm)
    {
        await OnSearch.InvokeAsync(searchTerm); 
    }
}