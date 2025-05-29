using Azure.Storage.Blobs;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed class RecipeImageAzUploader(BlobServiceClient blobServiceClient) : IImageUploader
{
    public async Task<string> UploadImage(
        string fileName,
        Stream stream,
        CancellationToken cancellationToken)
    {
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var containerName = "images";

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob, cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(uniqueFileName);
        var response = await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }
}