using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public interface IImageUploadService
{
    Task<Result<string>> UploadImage(
        Stream fileStream, 
        string fileName,
        long size, 
        string contentType,
        CancellationToken cancellationToken);
}
