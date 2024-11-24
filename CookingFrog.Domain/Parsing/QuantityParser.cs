using System.Globalization;
using System.Text.RegularExpressions;

namespace CookingFrog.Domain.Parsing;

public static class QuantityParser
{
    public static Quantity Parse(string quantity)
    {
        var match = Regex.Match(quantity, @"^(?<quantity>[0-9]{1,2}([.,]?[0-9]{0,1})) (?<unit>\w+)$");
        
        var number = match.Groups["quantity"].Value;
        var unit = match.Groups["unit"].Value;
        
        var parsedNumber = Convert.ToDecimal(number, new NumberFormatInfo { NumberDecimalSeparator = "." });
        var parsedUnit = Enum.Parse<UnitEnum>(unit, ignoreCase: true);
        
        return new Quantity(parsedNumber, parsedUnit);
    }
}