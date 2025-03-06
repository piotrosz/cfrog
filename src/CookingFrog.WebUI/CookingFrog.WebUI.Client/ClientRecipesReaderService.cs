using System.Text.Json;
using CookingFrog.Application.Models;

namespace CookingFrog.WebUI.Client;

public sealed class ClientRecipesReaderService(HttpClient httpClient) : IRecipesReaderService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries()
    {
        var response = await httpClient.GetStreamAsync(
            "api/summaries"); 
        return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeSummaryModel>>(
            response, JsonSerializerOptions) ?? [];       
    }

    public async Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm)
    {
        var response = await httpClient.GetStreamAsync(
            $"api/summaries?searchTerm={searchTerm}"); 
        return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeSummaryModel>>(
            response, JsonSerializerOptions) ?? []; 
    }

    public async Task<RecipeModel> GetRecipe(Guid recipeGuid)
    {
        var response = await httpClient.GetStreamAsync(
            $"api/recipes/{recipeGuid}");
        return await JsonSerializer.DeserializeAsync<RecipeModel>(
            response, JsonSerializerOptions) ?? throw new Exception("Recipe null!"); 
    }
}