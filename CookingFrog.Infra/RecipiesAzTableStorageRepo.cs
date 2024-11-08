using Azure.Data.Tables;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed class RecipesAzTableStorageRepo : IRecipesReadRepo
{
    private readonly TableServiceClient tableServiceClient;

    public RecipesAzTableStorageRepo(TableServiceClient tableServiceClient)
    {
        this.tableServiceClient = tableServiceClient;
    }

    public async Task<Recipe> GetRecipe(Guid guid)
    {
        
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<RecipeSummary>> GetRecipes()
    {
        throw new NotImplementedException();
    }
}
