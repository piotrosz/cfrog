using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CookingFrog.WebUI.Client.Models;

namespace CookingFrog.WebUI;

public sealed class ServerRecipesReaderService(IRecipesReader reader) : IRecipesReaderService
{
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipeSummaries()
    {
        return (await reader.GetRecipeSummaries())
            .Select(x => new RecipeSummaryModel(x.Summary, x.Guid));
    }

    public async Task<IEnumerable<RecipeSummaryModel>> QueryRecipeSummaries(string searchTerm)
    {
        return (await reader.QueryRecipeSummaries(searchTerm))
            .Select(x => new RecipeSummaryModel(x.Summary, x.Guid));
    }
}