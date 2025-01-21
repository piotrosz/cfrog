using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CookingFrog.WebUI.Client.Mapping;
using CookingFrog.WebUI.Client.Models;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI;

public sealed class ServerRecipesUpdaterService(IRecipesUpdater recipesUpdater) : IRecipesUpdaterService
{
    public Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.UpdateTitle(newTitle, recipeGuid, cancellationToken);
    }

    public Task UpdateIngredient(int index, IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.UpdateIngredient(index, ingredient.MapToDomain(), recipeGuid, cancellationToken);
    }

    public Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.UpdateStep(stepIndex, description, recipeGuid, cancellationToken);
    }

    public Task AddIngredient(IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.AddIngredient(ingredient.MapToDomain(), recipeGuid, cancellationToken);
    }

    public Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.DeleteIngredient(index, recipeGuid, cancellationToken);
    }

    public Task AddStep(int? index, string step, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.AddStep(index, step, recipeGuid, cancellationToken);
    }

    public Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        return recipesUpdater.DeleteStep(index, recipeGuid, cancellationToken);
    }
}