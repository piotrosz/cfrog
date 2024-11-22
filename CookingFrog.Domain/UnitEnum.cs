using System.ComponentModel.DataAnnotations;

namespace CookingFrog.Domain;

public enum UnitEnum
{
    [Display(Description = "")]
    Undefined,
    [Display(Description = "")]
    Quantity,
    /* Weight */
    [Display(Description = "kg")]
    Kilogram,
    [Display(Description = "g")]
    Gram,
    /* Other */
    [Display(Description = "łyżka")]
    Spoon,
    [Display(Description = "łyżeczka")]
    Teaspoon,
    [Display(Description = "szklanka")]
    Glass,
    [Display(Description = "szczypta")]
    Handful,
    [Display(Description = "ml")]
    Millilitre
}