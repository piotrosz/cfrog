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
            "puszki" => ParseResult<UnitEnum>.Success(UnitEnum.Can),
            "puszka" => ParseResult<UnitEnum>.Success(UnitEnum.Can),
            "puszek" => ParseResult<UnitEnum>.Success(UnitEnum.Can),
            "ząbek" => ParseResult<UnitEnum>.Success(UnitEnum.Clove),
            "ząbki" => ParseResult<UnitEnum>.Success(UnitEnum.Clove),
            "ząbków" => ParseResult<UnitEnum>.Success(UnitEnum.Clove),
            "szklanka" => ParseResult<UnitEnum>.Success(UnitEnum.Glass),
            "szklanki" => ParseResult<UnitEnum>.Success(UnitEnum.Glass),
            "szklanek" => ParseResult<UnitEnum>.Success(UnitEnum.Glass),
            "szczypta" => ParseResult<UnitEnum>.Success(UnitEnum.Handful),
            "szczypty" => ParseResult<UnitEnum>.Success(UnitEnum.Handful),
            "szczypt" => ParseResult<UnitEnum>.Success(UnitEnum.Handful),
            "łyżka" => ParseResult<UnitEnum>.Success(UnitEnum.Spoon),
            "łyżki" => ParseResult<UnitEnum>.Success(UnitEnum.Spoon),
            "łyżek" => ParseResult<UnitEnum>.Success(UnitEnum.Spoon),
            "łyżeczka" => ParseResult<UnitEnum>.Success(UnitEnum.Teaspoon),
            "łyżeczki" => ParseResult<UnitEnum>.Success(UnitEnum.Teaspoon),
            "łyżeczek" => ParseResult<UnitEnum>.Success(UnitEnum.Teaspoon),
            "kawałek" => ParseResult<UnitEnum>.Success(UnitEnum.Piece),
            "kawałki" => ParseResult<UnitEnum>.Success(UnitEnum.Piece),
            "kawałków" => ParseResult<UnitEnum>.Success(UnitEnum.Piece),
            _ => ParseResult<UnitEnum>.Error($"Cannot parse unit: '{input}'")
        };
    }
}