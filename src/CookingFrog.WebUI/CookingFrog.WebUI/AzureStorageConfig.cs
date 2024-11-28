namespace CookingFrog.WebUI;

public sealed class AzureStorageConfig
{
    public required Uri Uri { get; init; }
        
    public required string AccountName { get; init; }
    
    public required string AccountKey { get; init; }
}