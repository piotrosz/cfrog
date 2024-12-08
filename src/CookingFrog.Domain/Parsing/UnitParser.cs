namespace CookingFrog.Domain.Parsing;

public static class UnitParser
{
    public static ParseResult<UnitEnum> Parse(string input)
    {
        var unitParseSuccess = Enum.TryParse<UnitEnum>(input, ignoreCase: true, out var result);

        if (unitParseSuccess)
        {
            return ParseResult<UnitEnum>.Success(result);
        }

        return input switch
        {
            "g" => ParseResult<UnitEnum>.Success(UnitEnum.Gram),
            "ml" => ParseResult<UnitEnum>.Success(UnitEnum.Millilitre),
            "kg" => ParseResult<UnitEnum>.Success(UnitEnum.Kilogram),
            "puszka" => ParseResult<UnitEnum>.Success(UnitEnum.Can),
            "ząbek" => ParseResult<UnitEnum>.Success(UnitEnum.Clove),
            "szkanka" => ParseResult<UnitEnum>.Success(UnitEnum.Glass),
            "szczypta" => ParseResult<UnitEnum>.Success(UnitEnum.Handful),
            "łyżka" => ParseResult<UnitEnum>.Success(UnitEnum.Spoon),
            "łyżeczka" => ParseResult<UnitEnum>.Success(UnitEnum.Teaspoon),
            _ => ParseResult<UnitEnum>.Error($"Cannot parse unit: '{input}'")
        };
    }
}