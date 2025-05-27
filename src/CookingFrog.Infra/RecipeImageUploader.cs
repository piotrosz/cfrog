using Azure.Storage.Blobs;
using CookingFrog.Domain;

public sealed class RecipeImageUploader(BlobServiceClient blobServiceClient) : IImageUploader
{
    public async Task<string> UploadImage(
        string fileName,
        Stream stream,
        CancellationToken cancellationToken)
    {
        // Create unique file name
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var containerName = "images";

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

        var blobClient = containerClient.GetBlobClient(uniqueFileName);
        var response = await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }
}