using System.Net.Http.Json;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public sealed class ClientRecipesUpdaterService(HttpClient httpClient) : IRecipesUpdaterService
{
    public async Task<Result> UpdateTitle(string newTitle, Guid recipeGuid, CancellationToken cancellationToken)
    {
        var result = await httpClient.PutAsJsonAsync($"/api/recipes/{recipeGuid}/title", 
            new StringContent(newTitle), cancellationToken);
        
        result.EnsureSuccessStatusCode();
        
        return Result.Success();
    }

    public Task UpdateIngredient(int index, Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStep(int stepIndex, string description, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddIngredient(Ingredient ingredient, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIngredient(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddStep(int? index, Step step, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStep(int index, Guid recipeGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}