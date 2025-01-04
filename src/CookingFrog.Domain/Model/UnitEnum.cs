using System.ComponentModel.DataAnnotations;

namespace CookingFrog.Domain;

public enum UnitEnum
{
    [Display(Description = "")]
    Undefined,
    [Display(Description = "")]
    Quantity,
    [Display(Description = "kg")]
    Kilogram,
    [Display(Description = "g")]
    Gram,
    [Display(Description = "łyżka")]
    Spoon,
    [Display(Description = "łyżeczka")]
    Teaspoon,
    [Display(Description = "szklanka")]
    Glass,
    [Display(Description = "szczypta")]
    Handful,
    [Display(Description = "ml")]
    Millilitre,
    [Display(Description = "ząbek")]
    Clove,
    [Display(Description = "puszka")]
    Can,
    [Display(Description = "kawałek")]
    Piece
}