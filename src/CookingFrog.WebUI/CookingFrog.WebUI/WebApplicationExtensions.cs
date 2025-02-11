using CookingFrog.Domain;
using CookingFrog.WebUI.Client.Mapping;
using CookingFrog.WebUI.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookingFrog.WebUI;

internal static class WebApplicationExtensions
{
    public static void UseCfrogMinimalApi(this WebApplication app)
    {
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
}