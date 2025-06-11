using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed class RecipeImageAzUploader(BlobServiceClient blobServiceClient) : IImageUploader
{
    public async Task<Uri> UploadImage(
        string fileName,
        Stream stream,
        CancellationToken cancellationToken)
    {
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        const string containerName = "images";
        
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);
        
        var blobClient = containerClient.GetBlobClient(uniqueFileName);
        await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);
    
        return blobClient.Uri;
    }
}