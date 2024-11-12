using CookingFrog.Domain;

namespace CookingFrog.Infra;

public class StaticRecipesLoader(IRecipesPersistRepo persistRepo, IRecipesReadRepo readRepo)
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        foreach (var recipeSummary in await readRepo.GetRecipeSummaries())
        {
            await persistRepo.Save(recipeSummary, cancellationToken);
            await persistRepo.Save(await readRepo.GetRecipe(recipeSummary.Guid), cancellationToken);
        }
    }
}