namespace CookingFrog.Infra;

public interface IStaticRecipesLoader
{
    Task Load(CancellationToken cancellationToken = default);

}