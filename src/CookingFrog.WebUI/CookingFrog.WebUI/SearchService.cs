namespace CookingFrog.WebUI;

public class SearchService
{
    public required Func<string, Task> OnSearch { get; set; }

    public void NotifySearch(string searchTerm)
    {
        OnSearch?.Invoke(searchTerm);
    }
}