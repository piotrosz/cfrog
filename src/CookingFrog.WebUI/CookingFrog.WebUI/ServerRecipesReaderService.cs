using CookingFrog.Application.Models;
using CookingFrog.Domain;
using CookingFrog.WebUI.Client;

namespace CookingFrog.WebUI;

public sealed class ServerRecipesReaderService(IRecipesReader reader) : IRecipesReaderService
{
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries()
    {
        return (await reader.GetRecipeSummaries())
            .Select(x => x.MapToRecipeSummaryModel());
    }

    public async Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm)
    {
        return (await reader.QueryRecipeSummaries(searchTerm))
            .Select(x => x.MapToRecipeSummaryModel());
    }

    public async Task<RecipeModel> GetRecipe(Guid recipeGuid)
    {
        return (await reader.GetRecipe(recipeGuid)).MapToRecipeModel();
    }
}