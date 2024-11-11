using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components.Pages;

public partial class RecipeAdd
{
    [SupplyParameterFromForm]
    private RecipeAddModel? Model { get; set; }

    protected override void OnInitialized() => Model ??= new RecipeAddModel();
    
    private void Submit()
    {
        
    }

    public class RecipeAddModel
    {
        public string? Title { get; set; }
        public string? Ingredients { get; set; }
        public string? Steps { get; set; }
    }
}