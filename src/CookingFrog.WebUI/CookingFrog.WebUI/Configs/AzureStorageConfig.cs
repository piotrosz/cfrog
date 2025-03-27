namespace CookingFrog.WebUI;

public sealed class AzureStorageConfig
{
    public Uri? Uri { get; init; }
        
    public string? AccountName { get; init; }
    
    public string? AccountKey { get; init; }

    /// <summary>
    /// Either the connection string or the account key must be provided. 
    /// </summary>
    public string? ConnectionString { get; init; }
}