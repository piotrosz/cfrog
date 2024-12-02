using System.Globalization;
using System.Text.RegularExpressions;

namespace CookingFrog.Domain.Parsing;

public static class QuantityParser
{
    public static ParseResult<Quantity> Parse(string quantity)
    {
        if (string.IsNullOrWhiteSpace(quantity))
        {
            ParseResult<Quantity>.Error("Quantity cannot be empty.");
        }
        
        var match = Regex.Match(quantity, @"^(?<quantity>[0-9]{1,2}([.,]?[0-9]{0,1})) (?<unit>\w+)$");

        if (!match.Success)
        {
            ParseResult<Quantity>.Error("Quantity cannot be parsed.");
        }
        
        var number = match.Groups["quantity"].Value;
        var unit = match.Groups["unit"].Value;
        
        var parsedNumber = Convert.ToDecimal(number, new NumberFormatInfo { NumberDecimalSeparator = "." });
        var unitParseSuccess = Enum.TryParse<UnitEnum>(unit, ignoreCase: true, out var result);

        return !unitParseSuccess ? 
            ParseResult<Quantity>.Error("Unit cannot be parsed.") : 
            ParseResult<Quantity>.Success(new Quantity(parsedNumber, result));
    }
}