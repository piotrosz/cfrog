using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed class RecipesAzReadRepo(TableServiceClient tableServiceClient) : IRecipesReadRepo
{
    public async Task<Recipe> GetRecipe(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<RecipeSummary>> GetRecipes()
    {
        throw new NotImplementedException();
    }
}
