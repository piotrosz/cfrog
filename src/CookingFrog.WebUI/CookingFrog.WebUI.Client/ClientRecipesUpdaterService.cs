using System.Net.Http.Json;
using CookingFrog.WebUI.Client.Models;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public sealed class ClientRecipesUpdaterService(HttpClient httpClient) : IRecipesUpdaterService
{
    public async Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.PutAsJsonAsync(
            $"/api/recipes/{recipeGuid}/title", 
            newTitle, 
            cancellationToken);
        result.EnsureSuccessStatusCode();
        return Result.Success();
    }

    public async Task UpdateIngredient(int index, IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.PutAsJsonAsync(
            $"api/recipes/{recipeGuid}/ingredients/{index}",
            ingredient, cancellationToken);
        result.EnsureSuccessStatusCode();
    }

    public async Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.PutAsJsonAsync(
            $"api/recipes/{recipeGuid}/steps/{stepIndex}",
            description, cancellationToken);
        result.EnsureSuccessStatusCode();
    }

    public async Task AddIngredient(IngredientModel ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.PostAsJsonAsync(
            $"api/recipes/{recipeGuid}/ingredients",
            ingredient, 
            cancellationToken);
        result.EnsureSuccessStatusCode();
    }

    public async Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.DeleteAsync(
            $"api/recipes/{recipeGuid}/ingredients/{index}",
            cancellationToken);
        result.EnsureSuccessStatusCode();
    }

    public async Task AddStep(int? index, string step, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var url = index.HasValue ?
            $"api/recipes/{recipeGuid}/steps/{index}" :
            $"api/recipes/{recipeGuid}/steps";
        
        var result = await httpClient.PostAsJsonAsync(
            url,
            step, 
            cancellationToken);
        result.EnsureSuccessStatusCode();
    }

    public async Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.DeleteAsync(
            $"api/recipes/{recipeGuid}/steps/{index}",
            cancellationToken);
        result.EnsureSuccessStatusCode();
    }
}