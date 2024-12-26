﻿using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    private Recipe? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await RecipesReadRepo.GetRecipe(guid);
        }
    }

    [Parameter] 
    public string? Guid { get; set; }
    
    private async Task UpdateSummary(string? newSummary)
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(newSummary))
        {
            await RecipesUpdateRepo.UpdateTitle(newSummary, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReadRepo.GetRecipe(Recipe.Guid);
        }
    }
}