﻿using CookingFrog.Domain;
using CookingFrog.WebUI.Client.Models;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public interface IRecipesUpdaterService
{
    Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken);

    Task UpdateIngredient(int index, IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken);

    Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken);

    Task AddIngredient(IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task AddStep(int? index, string step, Guid recipeGuid, CancellationToken cancellationToken);
    
    Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken);
}