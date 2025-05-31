using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI;

public sealed class ServerImageUploadService(IImageUploader imageUploader) : IImageUploadService
{
    public async Task<Result<string>> UploadImage(
        Stream fileStream, 
        string fileName, 
        long size,
        string contentType,
        CancellationToken cancellationToken)
    {
        var imageUrl = await imageUploader.UploadImage(
            fileName,
            fileStream,
            CancellationToken.None);

        return imageUrl;
    }
}