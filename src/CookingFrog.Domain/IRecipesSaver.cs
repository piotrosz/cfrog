using CSharpFunctionalExtensions;

namespace CookingFrog.Domain;

public interface IRecipesSaver
{
    Task<Result> SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default);
}
