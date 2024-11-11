namespace CookingFrog.Domain;

public interface IRecipesPersistRepo
{
    Task Save(Recipe recipe, CancellationToken cancellationToken = default);
    Task Save(RecipeSummary recipeSummary, CancellationToken cancellationToken = default);

}