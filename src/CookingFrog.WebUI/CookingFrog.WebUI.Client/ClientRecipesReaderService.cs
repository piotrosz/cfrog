using System.Text.Json;
using CookingFrog.WebUI.Client.Models;

namespace CookingFrog.WebUI.Client;

public sealed class ClientRecipesReaderService(HttpClient httpClient) : IRecipesReaderService
{
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries()
    {
        var response = await httpClient.GetStreamAsync(
            "api/recipes"); 
        return await JsonSserializer.DeserializeAsync<IEnumerable<RecipeSummaryModel>>(
            response, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true});       
    }

    public async Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm)
    {
        var response = await httpClient.GetStreamAsync(
            $"api/recipes?searchTerm={searchTerm}"); 
        return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeSummaryModel>>(
            response, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true}); 
    }
}