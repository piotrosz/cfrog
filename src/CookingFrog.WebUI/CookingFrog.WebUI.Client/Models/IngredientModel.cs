using CookingFrog.Domain;

namespace CookingFrog.WebUI.Client.Models;

public record IngredientModel(string Name, decimal Amount, UnitEnum Unit, string? GroupName = null, int? Alternative = null);