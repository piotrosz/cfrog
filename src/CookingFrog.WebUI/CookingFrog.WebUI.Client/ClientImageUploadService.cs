using System.Net.Http.Headers;
using CookingFrog.Domain;
using CSharpFunctionalExtensions;

namespace CookingFrog.WebUI.Client;

public sealed class ClientImageUploadService(HttpClient httpClient) : IImageUploadService
{
    private const int MaxAllowedFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    public async Task<Result<string>> UploadImage(
        Stream fileStream, 
        string fileName,
        long size,
        string contentType, 
        CancellationToken cancellationToken)
    {
        try
        {
            // Validate file size
            if (size > MaxAllowedFileSizeInBytes)
                return Result.Failure<string>($"File size exceeds the maximum allowed size of {MaxAllowedFileSizeInBytes / (1024 * 1024)} MB.");

            // Validate file type
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!AllowedImageExtensions.Contains(fileExtension))
                return Result.Failure<string>($"File type '{fileExtension}' is not allowed. Allowed types: {string.Join(", ", AllowedImageExtensions)}");

            // Prepare the content for upload
            using var content = new MultipartFormDataContent();
            
            // Get stream from the file
            using var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
            // Add the file to the form data
            content.Add(streamContent, "file", fileName);

            // Send the request
            var response = await httpClient.PostAsync("/api/images/upload", content, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            // Get the URL of the uploaded image
            var imageUrl = await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Success(imageUrl);
        }
        catch (Exception ex)
        {
            return Result.Failure<string>($"Error uploading image: {ex.Message}");
        }
    }
}
