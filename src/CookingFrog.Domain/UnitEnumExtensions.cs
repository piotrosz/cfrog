using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CookingFrog.Domain;

public static class UnitEnumExtensions
{
    public static string GetDisplayDescription(this UnitEnum enumValue, decimal quantity)
    {
        var desc = (enumValue.GetType().GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()!)
            .GetDescription() ?? "unknown";

        if (desc.Contains('|'))
        {
            var variations = desc.Split('|');
            return quantity switch
            {
                < 1 => variations[1],
                1 => variations[0],
                > 1 and <= 4 => variations[1],
                >= 5 => variations[2],
                _ => desc
            };
        }
        else
        {
            return desc;
        }
    }
}