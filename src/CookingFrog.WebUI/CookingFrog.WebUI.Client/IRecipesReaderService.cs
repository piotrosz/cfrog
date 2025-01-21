using CookingFrog.WebUI.Client.Models;

namespace CookingFrog.WebUI.Client;

public interface IRecipesReaderService
{
    Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries();

    Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm);
    
    Task<RecipeModel> GetRecipe(Guid recipeGuid);
}