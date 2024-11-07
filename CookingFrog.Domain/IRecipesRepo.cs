namespace CookingFrog.Domain;

public interface IRecipesRepo
{
    IReadOnlyList<RecipeSummary> GetRecipes();
    Recipe GetRecipe(Guid guid);
}