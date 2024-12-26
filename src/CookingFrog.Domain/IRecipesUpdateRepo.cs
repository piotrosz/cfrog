using CSharpFunctionalExtensions;

namespace CookingFrog.Domain;

public interface IRecipesUpdateRepo
{
    Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken);

    Task UpdateIngredients(IReadOnlyCollection<Ingredient> ingredients, Guid recipeGuid);

    Task UpdateSteps(IReadOnlyCollection<Step> steps, Guid recipeGuid);

}