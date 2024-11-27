namespace CookingFrog.WebUI;

public class AzureStorageConfig
{
    public required Uri Uri { get; init; }
        
    public required string AccountName { get; init; }
    
    public required string AccountKey { get; init; }
}