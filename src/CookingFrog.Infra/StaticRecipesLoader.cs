using CookingFrog.Domain;
using Microsoft.Extensions.Logging;

namespace CookingFrog.Infra;

public class StaticRecipesLoader(
    IRecipesPersistRepo persistRepo, 
    IRecipesReadRepo readRepo,
    ILogger<StaticRecipesLoader> logger) : IStaticRecipesLoader
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Loading Static Recipes");
        foreach (var recipeSummary in await readRepo.GetRecipeSummaries())
        {
            await persistRepo.SaveRecipeSummaryOnly(recipeSummary, cancellationToken);
            await persistRepo.SaveRecipeOnly(await readRepo.GetRecipe(recipeSummary.Guid), cancellationToken);
        }
    }
}