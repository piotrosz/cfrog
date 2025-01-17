namespace CookingFrog.Domain;

public interface IRecipesReader
{
    Task<IReadOnlyList<RecipeSummary>> GetRecipeSummaries();

    Task<IReadOnlyList<RecipeSummary>> QueryRecipeSummaries(string searchTerm);

    Task<Recipe> GetRecipe(Guid guid);
}