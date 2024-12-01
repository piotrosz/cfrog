using System.ComponentModel.DataAnnotations;
using CookingFrog.Domain.Parsing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeAdd
{
    [SupplyParameterFromForm]
    private RecipeAddModel? Model { get; set; }
    
    private EditContext? editContext;

    private ValidationMessageStore? messageStore;

    protected override void OnInitialized()
    {
        Model ??= new RecipeAddModel();
        editContext = new(Model);
        editContext.OnValidationRequested += HandleValidationRequested;
        messageStore = new(editContext);
    } 
    
    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        messageStore?.Clear();

        // Custom validation logic
        if (Model != null && Model.Ingredients.Any() )
        {
            messageStore?.Add(() => Model.Ingredients, "Add at least one.");
        }
    }

    private void SubmitInvalid()
    {
        
    }
    
    private void SubmitValid()
    {
        var recipe = RecipeParser.Parse(
            Model!.TimeToPrepare,
            Model.Title,
            Model.Ingredients,
            Model.Steps);
    }

    public void Dispose()
    {
        if (editContext is not null)
        {
            editContext.OnValidationRequested -= HandleValidationRequested;
        }
    }
    
    public sealed class RecipeAddModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Title minimal length is 10")]
        [MaxLength(400, ErrorMessage = "Title maximal length is 400")]
        public string Title { get; set; } = "";

        [Required]
        public string Ingredients { get; set; } = "";

        [Required]
        public string Steps { get; set; } = "";

        [Required]
        [RegularExpression(@"\d{1,2}:\d{1,2}")]
        public string TimeToPrepare { get; set; } = "00:00";
    }
}