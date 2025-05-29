using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public interface IImageUploadService
{
    Task<Result<string>> UploadImage(IBrowserFile file, CancellationToken cancellationToken);
}
