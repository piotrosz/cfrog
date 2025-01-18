using System.Text.Json;
using CookingFrog.WebUI.Client.Models;

namespace CookingFrog.WebUI.Client;

public class ClientRecipesReaderService(HttpClient httpClient) : IRecipesReaderService
{
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries()
    {
        var response = await httpClient.GetStreamAsync(
            "api/recipes"); 
        return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeSummaryModel>>(
            response, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true});       
    }

    public Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm)
    {
        throw new NotImplementedException();
    }
}