﻿namespace CookingFrog.Domain;

public interface IRecipesUpdateRepo
{
    Task UpdateTitle(string newTitle, Guid recipeGuid);

    Task UpdateIngredients(IReadOnlyCollection<Ingredient> ingredients, Guid recipeGuid);

    Task UpdateSteps(IReadOnlyCollection<Step> steps, Guid recipeGuid);

}