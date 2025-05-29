using System.ComponentModel.DataAnnotations;
using CookingFrog.Domain;
using CookingFrog.Domain.Parsing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeAdd(
    IRecipesSaver? RecipesRepo,
    NavigationManager? NavigationManager)
{
    [SupplyParameterFromForm]
    private RecipeAddModel? Model { get; set; }
    
    private EditContext? _editContext;

    private ValidationMessageStore? _messageStore;

    private string? _saveError;

    protected override void OnInitialized()
    {
        Model ??= new RecipeAddModel();
        _editContext = new(Model);
        _editContext.OnValidationRequested += HandleValidationRequested;
        _messageStore = new(_editContext);
    } 
    
    private void HandleValidationRequested(
        object? sender,
        ValidationRequestedEventArgs args)
    {
        _messageStore?.Clear();

        var parseResult = RecipeParser.Parse(
            Model!.TimeToPrepare,
            Model.Title,
            Model.Ingredients,
            Model.Steps,
            Model.ImageUrl,
            Model.Notes);

        if (!parseResult.IsSuccess)
        {
            switch (parseResult.Part)
            {
                case "ingredients":
                    _messageStore?.Add(() => Model.Ingredients, parseResult.ErrorDescription!);
                    break;
                case "steps":
                    _messageStore?.Add(() => Model.Steps, parseResult.ErrorDescription!);
                    break;
            }
        }
    }

    private void SubmitInvalid()
    {
        
    }
    
    private async Task SubmitValid()
    {
        var parseResult = RecipeParser.Parse(
            Model!.TimeToPrepare,
            Model.Title,
            Model.Ingredients,
            Model.Steps,
            Model.ImageUrl,
            Model.Notes);

        if (parseResult.IsSuccess)
        {
            var saveResult = await RecipesRepo!.SaveRecipe(parseResult.Result);
            if (saveResult.IsSuccess)
            {
                NavigationManager!.NavigateTo("/");
            }
            else
            {
                _saveError = saveResult.Error;
            }
        }
    }

    public void Dispose()
    {
        if (_editContext is not null)
        {
            _editContext.OnValidationRequested -= HandleValidationRequested;
        }
    }

    public sealed class RecipeAddModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title minimal length is 5")]
        [MaxLength(400, ErrorMessage = "Title maximal length is 400")]
        public string Title { get; set; } = "";

        [Required]
        public string Ingredients { get; set; } = "0.5 kg; mąka \r\n1; cebula";

        [Required]
        public string Steps { get; set; } = "Każdy krok w nowej linii.";

        [Required]
        [RegularExpression(@"\d{1,2}:\d{1,2}")]
        public string TimeToPrepare { get; set; } = "00:00";

        public string? ImageUrl { get; set; }
        
        public string? Notes { get; set; }
    }
}