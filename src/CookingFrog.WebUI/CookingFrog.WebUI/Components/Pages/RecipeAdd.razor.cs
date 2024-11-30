using System.ComponentModel.DataAnnotations;
using CookingFrog.Domain.Parsing;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeAdd
{
    [SupplyParameterFromForm]
    private RecipeAddModel? Model { get; set; }

    protected override void OnInitialized() => Model ??= new RecipeAddModel();
    
    private async Task Submit()
    {
        var recipe = RecipeParser.Parse(
            Model.TimeToPrepare,
            Model.Title,
            Model.Ingredients,
            Model.Steps);
    }

    public class RecipeAddModel
    {
        [Required]
        [StringLength(10)]
        public string? Title { get; set; }
        
        [Required]
        public string? Ingredients { get; set; }
        
        [Required]
        public string? Steps { get; set; }
        
        [Required]
        [RegularExpression(@"\d{1,2}:\d{1,2}")]
        public string TimeToPrepare { get; set; }
    }
}