using CookingFrog.Domain;
using CookingFrog.WebUI.Client;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI;

public sealed class ServerImageUploadService(IImageUploader imageUploader) : IImageUploadService
{
    public async Task<Result<string>> UploadImage(IBrowserFile file, CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();
        var imageUrl = await imageUploader.UploadImage(
            file.Name,
            stream,
            CancellationToken.None);

        return imageUrl;
    }
}