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
        ConfigureRecipeApi(app);

        app.MapGet("/api/summaries",
            async ([FromQuery] string searchTerm, IRecipesReader reader) =>
            {
                var result = string.IsNullOrWhiteSpace(searchTerm)
                    ? await reader.GetRecipeSummaries()
                    : await reader.QueryRecipeSummaries(searchTerm);
                return result.Select(x => x.MapToRecipeSummaryModel());
            });
    }

    private static void ConfigureRecipeApi(WebApplication app)
    {
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

    private static void ConfigureImageUploadEndpoint(WebApplication app)
    {
        app.MapPost("/api/images/upload", async (
            HttpRequest request,
            [FromServices] IImageUploader imageUploader) =>
        {
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

                // Validate file size (e.g., max 5 MB)
                const long maxFileSize = 5 * 1024 * 1024; // 5 MB
                if (file.Length > maxFileSize)
                {
                    return Results.BadRequest("File size exceeds the maximum limit of 5 MB.");
                }

                using var stream = file.OpenReadStream();
                var imageUrl = await imageUploader.UploadImage(
                    file.FileName,
                    stream,
                    CancellationToken.None);

                return Results.Ok(imageUrl);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error uploading image: {ex.Message}");
            }
        });
    }
}