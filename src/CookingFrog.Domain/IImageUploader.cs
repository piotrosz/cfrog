namespace CookingFrog.Domain;

public interface IImageUploader
{
    Task<Uri> UploadImage(
        string fileName,
        Stream stream,
        CancellationToken cancellationToken = default);
}