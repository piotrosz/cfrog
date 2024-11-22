using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed partial class RecipesStaticTestRepo : IRecipesReadRepo
{
    public Task<IReadOnlyList<RecipeSummary>> GetRecipeSummaries()
    {
        return Task.FromResult<IReadOnlyList<RecipeSummary>>(Recipes
            .Select(r => new RecipeSummary(r.Guid, r.Summary))
            .ToList());
    }

    public Task<Recipe> GetRecipe(Guid guid)
    {
        return Task.FromResult(Recipes.Single(r => r.Guid == guid));
    }
}