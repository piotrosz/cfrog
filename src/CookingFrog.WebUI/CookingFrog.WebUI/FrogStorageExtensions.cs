namespace CookingFrog.WebUI;
using CookingFrog.Infra;

public static class FrogStorageExtensions
{
    public static void AddFrogStorage(this WebApplicationBuilder builder)
    {
        var azureStorageConfig = builder.Configuration
            .GetSection("Azure:Storage")
            .Get<AzureStorageConfig>();

        if (azureStorageConfig == null)
        {
            throw new InvalidOperationException("Azure Storage is not configured.");
        }

        if (!string.IsNullOrEmpty(azureStorageConfig.ConnectionString))
        {
            Console.WriteLine($"Adding Azure Storage with the connection string '{azureStorageConfig.ConnectionString}'.");
            builder.Services.AddFrogStorage(
                azureStorageConfig.ConnectionString);
        }
        else
        {
            Console.WriteLine("Adding Azure Storage with account key.");
            builder.Services.AddFrogStorage(
                azureStorageConfig.TableStorageUrl,
                azureStorageConfig.BlobStorageUrl,
                azureStorageConfig.AccountName,
                azureStorageConfig.AccountKey);
        }
    }
}
