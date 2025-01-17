using CookingFrog.Domain;
using CookingFrog.WebUI.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeDetail
{
    private bool _showEditButtons;
    
    [Inject]
    public IRecipesReader? RecipesReadRepo { get; set; }
        
    [Inject]
    public IRecipesUpdater? RecipesUpdateRepo { get; set; }
    
    [Inject] 
    public required IJSRuntime JsRuntime { get; set; }
    
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
        if (Recipe is not null)
        {
            await RecipesUpdateRepo!.UpdateStep(
                stepModel.Index, 
                stepModel.Description, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task UpdateIngredient(IngredientUpdateModel ingredientModel)
    {
        if (Recipe is not null)
        {
            var ingredient = new Ingredient(
                ingredientModel.Name,
                new Quantity(ingredientModel.Quantity, ingredientModel.Unit))
            {
                GroupName = ingredientModel.GroupName
            };
            
            await RecipesUpdateRepo!.UpdateIngredient(
                ingredientModel.Index, 
                ingredient, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteIngredient(int index)
    {
        if (Recipe is not null)
        {
            await RecipesUpdateRepo!.DeleteIngredient(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteStep(int index)
    {
        if (Recipe is not null)
        {
            await RecipesUpdateRepo!.DeleteStep(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task ShowAddIngredientModal()
    {
        await JsRuntime.InvokeVoidAsync("showAddIngredientBootstrapModal");
    }
    
    private async Task AddIngredient(IngredientAddModel ingredientModel)
    {
        if (Recipe is not null)
        {
            await RecipesUpdateRepo!.AddIngredient(new Ingredient(
                ingredientModel.Name, 
                new Quantity(ingredientModel.Quantity, ingredientModel.Unit)),
                Recipe.Guid,
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task ShowAddStepModal()
    {
        await JsRuntime.InvokeVoidAsync("showAddStepBootstrapModal");
    }
    
    private async Task AddStep(AddStepModel step)
    {
        if (Recipe is not null)
        {
            await RecipesUpdateRepo!.AddStep(
                step.Index,
                new Step(step.Step), 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReadRepo!.GetRecipe(Recipe.Guid);
        }
    }
}