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
        
        var match = Regex.Match(quantity.Trim(), @"^(?<quantity>[0-9]{0,2}([.,]?[0-9]{0,1}))[ ]{0,1}(?<unit>\w+)?$");

        if (!match.Success)
        {
            return ParseResult<Quantity>.Error($"Quantity cannot be parsed: '{quantity}'.");
        }
        
        var number = match.Groups["quantity"].Value;
        var parsedNumber = 1m;
        if (!string.IsNullOrWhiteSpace(number))
        {
            parsedNumber = Convert.ToDecimal(number, new NumberFormatInfo { NumberDecimalSeparator = "." });
        }
        
        var unit = match.Groups["unit"].Value;
        if (string.IsNullOrEmpty(unit))
        {
            return ParseResult<Quantity>.Success(new Quantity(parsedNumber, UnitEnum.Quantity));
        }
        
        var unitParseResult = UnitParser.Parse(unit);

        return !unitParseResult.IsSuccess ? 
            ParseResult<Quantity>.Error($"Quantity cannot be parsed: '{quantity}'.") : 
            ParseResult<Quantity>.Success(new Quantity(parsedNumber, unitParseResult.Result));
    }
}