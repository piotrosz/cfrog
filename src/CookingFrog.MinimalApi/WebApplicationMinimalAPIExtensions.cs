using Azure.Storage.Blobs;
using CookingFrog.Application.Models;
using CookingFrog.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CookingFrog.MinimalApi;

public static class WebApplicationExtensions
{
    public static void UseCfrogMinimalApi(this WebApplication app)
    {
        ConfigureImageUploadEndpoint(app);
        
        app.MapGet("/api/summaries", 
            async ([FromQuery] string searchTerm, IRecipesReader reader) =>
        {
            var result = string.IsNullOrWhiteSpace(searchTerm)
                ? await reader.GetRecipeSummaries()
                : await reader.QueryRecipeSummaries(searchTerm);
            return result.Select(x => x.MapToRecipeSummaryModel());
        });

        app.MapGet("/api/recipes/{guid}",
            async (Guid guid, IRecipesReader reader) =>
            {
                var recipe = await reader.GetRecipe(guid);
                return recipe.MapToRecipeModel();
            });

        app.MapPut("api/recipes/{guid}/title",
            async (Guid guid, [FromBody] string title, IRecipesUpdater updater) =>
            {
                await updater.UpdateTitle(title, guid, CancellationToken.None);
            });

        app.MapPut("api/recipes/{guid}/note",
            async (Guid guid, [FromBody] string note, IRecipesUpdater updater) =>
            {
                await updater.UpdateNotes(note, guid, CancellationToken.None);
            });
        
        app.MapPut("api/recipes/{guid}/ingredients/{index}",
            async (Guid guid, int index, [FromBody] IngredientModel ingredient, IRecipesUpdater updater) =>
            {
                await updater.UpdateIngredient(index, ingredient.MapToDomain(), guid, CancellationToken.None);
            });

        app.MapPut("api/recipes/{guid}/steps/{index}",
            async (Guid guid, int index, [FromBody] string step, IRecipesUpdater updater) =>
            {
                await updater.UpdateStep(index, step, guid, CancellationToken.None);
            });

        app.MapPost("api/recipes/{guid}/ingredients",
            async (Guid guid, [FromBody] IngredientModel ingredient, IRecipesUpdater updater) =>
            {
                await updater.AddIngredient(ingredient.MapToDomain(), guid, CancellationToken.None);
            });

        app.MapPost("api/recipes/{guid}/steps/{index}",
            async (Guid guid, int index, [FromBody] string step, IRecipesUpdater updater) =>
            {
                await updater.AddStep(index, step, guid, CancellationToken.None);
            });

        app.MapPost("api/recipes/{guid}/steps/",
            async (Guid guid, [FromBody] string step, IRecipesUpdater updater) =>
            {
                await updater.AddStep(index: null, step, guid, CancellationToken.None);
            });

        app.MapDelete("api/recipes/{guid}/steps/{index}",
            async (Guid guid, int index, IRecipesUpdater updater) =>
            {
                await updater.DeleteStep(index, guid, CancellationToken.None);
            });

        app.MapDelete("api/recipes/{guid}/ingredients/{index}",
            async (Guid guid, int index, IRecipesUpdater updater) =>
            {
                await updater.DeleteIngredient(index, guid, CancellationToken.None);
            });
    }


    // TODO: Should not contain direct dependencies on Azure.Storage.Blobs
    private static void ConfigureImageUploadEndpoint(WebApplication app)
    {
        app.MapPost("/api/images/upload", async (HttpRequest request, IConfiguration configuration) =>
        {
            // Get the connection string from configuration
            var connectionString = configuration["Azure:Storage:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                return Results.BadRequest("Blob storage connection string not configured");
            }

            try
            {
                // Check if the request has a file
                if (!request.HasFormContentType || request.Form.Files.Count == 0)
                {
                    return Results.BadRequest("No file was uploaded");
                }

                // Get the file from the request
                var file = request.Form.Files[0];
                if (file.Length == 0)
                {
                    return Results.BadRequest("File is empty");
                }

                // Validate file type
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(file.ContentType.ToLowerInvariant()))
                {
                    return Results.BadRequest("Invalid file type. Only jpg, png, and gif are allowed.");
                }

                // Create unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var containerName = "images";

                // Create blob client and container
                var blobServiceClient = new BlobServiceClient(connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Create the container if it doesn't exist
                await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                // Get blob client and upload the file
                var blobClient = containerClient.GetBlobClient(fileName);
                await using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, overwrite: true);

                // Return the URL of the uploaded image
                return Results.Ok(blobClient.Uri.ToString());
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error uploading image: {ex.Message}");
            }
        });
    }
}