namespace CookingFrog.Domain;

public interface IRecipesReadRepo
{
    Task<IReadOnlyList<RecipeSummary>> GetRecipeSummaries();
    Task<Recipe> GetRecipe(Guid guid);
}