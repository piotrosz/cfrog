using CookingFrog.Domain;

namespace CookingFrog.WebUI.WebAPI.Models;

public record IngredientModel(string Name, decimal Amount, UnitEnum Unit, string? GroupName = null);