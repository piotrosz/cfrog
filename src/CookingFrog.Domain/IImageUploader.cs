namespace CookingFrog.Domain;

public interface IImageUploader
{
    Task<string> UploadImage(
        string fileName,
        Stream stream,
        CancellationToken cancellationToken = default);
}