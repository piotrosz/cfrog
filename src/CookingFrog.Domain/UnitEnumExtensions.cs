using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CookingFrog.Domain;

public static class UnitEnumExtensions
{
    public static string GetDisplayDescription(this UnitEnum enumValue)
    {
        return (enumValue.GetType().GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()!)
            .GetDescription() ?? "unknown";
    }
}