using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes
{
    [Inject]
    public IRecipesReaderService RecipesReader { get; set; }
    
    [CascadingParameter] 
    public HttpContext? HttpContext { get; set; }
    
    private IQueryable<RecipeSummaryModel>? _recipes;
    
    protected override async Task OnInitializedAsync()
    {
        _recipes = (await RecipesReader.GetRecipeSummaries())
            .AsQueryable();
    }

    private async Task SearchRecipes(string searchTerm)
    {
        _recipes = (await RecipesReader.QueryRecipeSummaries(searchTerm))
            .AsQueryable();
    }
}