namespace CookingFrog.Domain;

public interface IRecipesReadRepo
{
    Task<IReadOnlyList<RecipeSummary>> GetRecipes();
    Task<Recipe> GetRecipe(Guid guid);
}