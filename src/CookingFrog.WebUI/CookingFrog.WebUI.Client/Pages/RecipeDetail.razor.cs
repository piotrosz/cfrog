using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Client.Pages;

public partial class RecipeDetail(
    IRecipesReaderService recipesReader,
    IRecipesUpdaterService recipesUpdater,
    IJSRuntime jsRuntime)
{
    private bool _showEditButtons;
    
    private RecipeModel? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await recipesReader.GetRecipe(guid);
        }
    }
    
    [Parameter] 
    public string? Guid { get; set; }
    
    private async Task UpdateSummary(string? newSummary)
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(newSummary))
        {
            await recipesUpdater.UpdateTitle(newSummary, Recipe.Guid, CancellationToken.None);
            Recipe = await recipesReader.GetRecipe(Recipe.Guid);
        }
    }

    private async Task UpdateNotes(string? newNote)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.UpdateNote(newNote, Recipe.Guid, CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }
    
    private async Task UpdateStep(StepUpdateModel stepModel)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.UpdateStep(
                stepModel.Index, 
                stepModel.Description, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task UpdateIngredient(IngredientUpdateModel ingredientModel)
    {
        if (Recipe is not null)
        {
            var ingredient = new IngredientModel(
                ingredientModel.Name,
                ingredientModel.Quantity,
                ingredientModel.Unit,
                ingredientModel.GroupName,
                ingredientModel.Alternative);
            
            await recipesUpdater!.UpdateIngredient(
                ingredientModel.Index, 
                ingredient, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteIngredient(int index)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.DeleteIngredient(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteStep(int index)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.DeleteStep(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task ShowAddIngredientModal()
    {
        await jsRuntime.InvokeVoidAsync("showAddIngredientBootstrapModal");
    }
    
    private async Task AddIngredient(IngredientAddModel ingredientModel)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.AddIngredient(new IngredientModel(
                ingredientModel.Name, 
                ingredientModel.Quantity, 
                ingredientModel.Unit, 
                GroupName: null), // TODO: Add possibility to add with group 
                Recipe.Guid,
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task ShowAddStepModal()
    {
        await jsRuntime.InvokeVoidAsync("showAddStepBootstrapModal");
    }
    
    private async Task AddStep(AddStepModel step)
    {
        if (Recipe is not null)
        {
            await recipesUpdater!.AddStep(
                step.Index,
                step.Step, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await recipesReader!.GetRecipe(Recipe.Guid);
        }
    }
}