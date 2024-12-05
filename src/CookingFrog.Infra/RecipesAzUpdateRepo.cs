using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

internal class RecipesAzUpdateRepo(TableServiceClient tableServiceClient) : IRecipesUpdateRepo
{
    public async Task UpdateTitle(string newTitle, Guid recipeGuid)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateIngredients(IReadOnlyCollection<Ingredient> ingredients, Guid recipeGuid)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSteps(IReadOnlyCollection<Step> steps, Guid recipeGuid)
    {
        throw new NotImplementedException();
    }
}