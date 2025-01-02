using CSharpFunctionalExtensions;

namespace CookingFrog.Domain;

public interface IRecipesSaveRepo
{
    Task<Result> SaveRecipe(Recipe recipe, CancellationToken cancellationToken = default);
}
