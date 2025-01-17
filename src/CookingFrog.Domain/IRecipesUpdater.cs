using CSharpFunctionalExtensions;

namespace CookingFrog.Domain;

public interface IRecipesUpdater
{
    Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken);

    Task UpdateIngredient(int index, Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken);

    Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken);

    Task AddIngredient(Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task AddStep(int? index, Step step, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken);
}