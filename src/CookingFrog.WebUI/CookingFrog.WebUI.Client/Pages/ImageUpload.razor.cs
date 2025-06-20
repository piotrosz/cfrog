using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CookingFrog.WebUI.Client.Pages;

public partial class ImageUpload(
    IImageUploadService imageUploadService,
    IJSRuntime jsRuntime)
{
    private IBrowserFile? SelectedFile { get; set; }
    private string? UploadedImageUrl { get; set; }
    private bool IsUploading { get; set; }
    private string? ErrorMessage { get; set; }
    
    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        ErrorMessage = null;
        SelectedFile = e.File;
    }
    
    private async Task UploadFile()
    {
        if (SelectedFile is null)
            return;
        
        try
        {
            IsUploading = true;
            ErrorMessage = null;

            await using var stream = SelectedFile.OpenReadStream();
            var result = await imageUploadService.UploadImage(
                stream,
                SelectedFile.Name,
                SelectedFile.Size,
                SelectedFile.ContentType, 
                CancellationToken.None);
            
            if (result.IsSuccess)
            {
                UploadedImageUrl = result.Value;
                SelectedFile = null;
            }
            else
            {
                ErrorMessage = result.Error;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsUploading = false;
        }
    }
    
    private async Task CopyImageUrlToClipboard()
    {
        if (string.IsNullOrEmpty(UploadedImageUrl))
            return;
        
        await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", UploadedImageUrl);
    }
}
