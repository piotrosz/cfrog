namespace CookingFrog.Domain;

public interface IRecipesPersistRepo
{
    Task SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default);
    Task SaveRecipeOnly(Recipe recipe, CancellationToken cancellationToken = default);
    Task SaveRecipeSummaryOnly(RecipeSummary recipeSummary, CancellationToken cancellationToken = default);

}