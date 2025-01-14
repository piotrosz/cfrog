﻿using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class Recipes
{
    [CascadingParameter] 
    public HttpContext? HttpContext { get; set; }
    
    [Inject]
    public SearchService SearchService { get; set; }
    
    private IQueryable<RecipeSummary>? _recipes;
    
    protected override async Task OnInitializedAsync()
    {
        _recipes = (await RecipesRepo.GetRecipeSummaries()).AsQueryable();
        SearchService.OnSearch += async searchTerm =>
        {
            _recipes = (await RecipesRepo.QueryRecipeSummaries(searchTerm)).AsQueryable();
        };
    }
}