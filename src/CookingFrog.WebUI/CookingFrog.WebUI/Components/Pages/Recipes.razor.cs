using CookingFrog.Application.Models;
using CookingFrog.WebUI.Client;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes(IRecipesReaderService recipesReader)
{
    [CascadingParameter] 
    public HttpContext? HttpContext { get; set; }
    
    private IQueryable<RecipeSummaryModel>? _initialRecipes = new List<RecipeSummaryModel>().AsQueryable();

    private IQueryable<RecipeSummaryModel>? _recipes = new List<RecipeSummaryModel>().AsQueryable();
    
    protected override async Task OnInitializedAsync()
    {
        _recipes = (await recipesReader.GetRecipeSummaries())
            .AsQueryable();

        _initialRecipes = _recipes;
    }

    private Task SearchRecipes(string searchTerm)
    {
        _recipes = _initialRecipes.Where(x => x.Summary
                .Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .AsQueryable();

        return Task.CompletedTask;

        //_recipes = (await recipesReader.QueryRecipeSummaries(searchTerm))
        //    .AsQueryable();
    }
}