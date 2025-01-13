namespace CookingFrog.WebUI;

public class SearchService
{
    public required Action<string> OnSearch { get; set; }

    public void NotifySearch(string searchTerm)
    {
        OnSearch?.Invoke(searchTerm);
    }
}