namespace CookingFrog.Domain;

public interface IRecipesPersistRepo
{
    Task Save(Recipe recipe);
}