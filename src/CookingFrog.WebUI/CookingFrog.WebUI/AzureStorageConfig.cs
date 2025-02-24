namespace CookingFrog.WebUI;

public sealed class AzureStorageConfig
{
    public Uri? Uri { get; init; }
        
    public string? AccountName { get; init; }
    
    public string? AccountKey { get; init; }

    public string? ConnectionString { get; init; }
}