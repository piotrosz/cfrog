namespace CookingFrog.Domain;

public interface IRecipesRepo
{
    Task<IReadOnlyList<RecipeSummary>> GetRecipes();
    Task<Recipe> GetRecipe(Guid guid);
}