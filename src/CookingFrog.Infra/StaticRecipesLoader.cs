using CookingFrog.Domain;
using Microsoft.Extensions.Logging;

namespace CookingFrog.Infra;

public class StaticRecipesLoader(
    IRecipesSaveRepo saveRepo, 
    IRecipesReadRepo readRepo,
    ILogger<StaticRecipesLoader> logger) : IStaticRecipesLoader
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Loading Static Recipes");
        foreach (var recipeSummary in await readRepo.GetRecipeSummaries())
        {
            var recipe = await readRepo.GetRecipe(recipeSummary.Guid);
            await saveRepo.SaveRecipe(recipe, cancellationToken);
        }
        logger.LogInformation("Static Recipes Loaded");
    }
}