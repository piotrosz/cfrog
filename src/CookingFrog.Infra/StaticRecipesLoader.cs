using CookingFrog.Domain;
using Microsoft.Extensions.Logging;

namespace CookingFrog.Infra;

public class StaticRecipesLoader(
    IRecipesSaver saver, 
    IRecipesReader reader,
    ILogger<StaticRecipesLoader> logger) : IStaticRecipesLoader
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Loading Static Recipes");
        foreach (var recipeSummary in await reader.GetRecipeSummaries())
        {
            var recipe = await reader.GetRecipe(recipeSummary.Guid);
            await saver.SaveRecipe(recipe, cancellationToken);
        }
        logger.LogInformation("Static Recipes Loaded");
    }
}