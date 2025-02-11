using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Client.Pages;

public partial class RecipeDetail
{
    private bool _showEditButtons;
    
    [Inject]
    public IRecipesReaderService? RecipesReader { get; set; }
        
    [Inject]
    public IRecipesUpdaterService? RecipesUpdater { get; set; }
    
    [Inject] 
    public required IJSRuntime JsRuntime { get; set; }
    
    private RecipeModel? Recipe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Guid is not null && System.Guid.TryParse(Guid, out var guid))
        {
            Recipe = await RecipesReader!.GetRecipe(guid);
        }
    }

    private IEnumerable<IngredientModel> GetIngredientsGrouped()
    {
        if (Recipe is null)
        {
            return [];
        }
        return Recipe.Ingredients
            .OrderBy(x => x.Name)
            .GroupBy(x => x.GroupName)
            .Select(x => x.ToList())
            .SelectMany(x => x);
    }
    
    [Parameter] 
    public string? Guid { get; set; }
    
    private async Task UpdateSummary(string? newSummary)
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(newSummary))
        {
            await RecipesUpdater!.UpdateTitle(newSummary, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task UpdateNotes(string? newNote)
    {
        if (Recipe is not null && !string.IsNullOrWhiteSpace(newNote))
        {
            await RecipesUpdater!.UpdateNote(newNote, Recipe.Guid, CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
        }
    }
    
    private async Task UpdateStep(StepUpdateModel stepModel)
    {
        if (Recipe is not null)
        {
            await RecipesUpdater!.UpdateStep(
                stepModel.Index, 
                stepModel.Description, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
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
                ingredientModel.GroupName);
            
            await RecipesUpdater!.UpdateIngredient(
                ingredientModel.Index, 
                ingredient, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteIngredient(int index)
    {
        if (Recipe is not null)
        {
            await RecipesUpdater!.DeleteIngredient(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
        }
    }

    private async Task DeleteStep(int index)
    {
        if (Recipe is not null)
        {
            await RecipesUpdater!.DeleteStep(
                index, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
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
            await RecipesUpdater!.AddIngredient(new IngredientModel(
                ingredientModel.Name, 
                ingredientModel.Quantity, 
                ingredientModel.Unit, 
                GroupName: null), // TODO: Add possibility to add with group 
                Recipe.Guid,
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
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
            await RecipesUpdater!.AddStep(
                step.Index,
                step.Step, 
                Recipe.Guid, 
                CancellationToken.None);
            Recipe = await RecipesReader!.GetRecipe(Recipe.Guid);
        }
    }
}