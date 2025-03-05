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
    [Display(Description = "łyżka|łyżki|łyżek")]
    Spoon,
    [Display(Description = "łyżeczka|łyżeczki|łyżeczek")]
    Teaspoon,
    [Display(Description = "szklanka|szklanki|szklanek")]
    Glass,
    [Display(Description = "szczypta|szczypty|szczypt")]
    Handful,
    [Display(Description = "ml")]
    Millilitre,
    [Display(Description = "ząbek|ząbki|ząbków")]
    Clove,
    [Display(Description = "puszka|puszki|puszek")]
    Can,
    [Display(Description = "kawałek|kawałki|kawałków")]
    Piece,
    [Display(Description = "cm")]
    Cm,
    [Display(Description = "listek|listki|listków")]
    Leave,
    [Display(Description = "łodyga|łodygi|łodyg")]
    Stalk,
    [Display(Description = "ziarenko|ziarenka|ziarenek")]
    Grain
}