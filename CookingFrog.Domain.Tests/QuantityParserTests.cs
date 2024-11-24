using CookingFrog.Domain.Parsing;

namespace CookingFrog.Domain.Tests;

public class QuantityParserTests
{
    [Fact]
    public void TestParseSuccess()
    {
        AssertParse("1 gram", new Quantity(1, UnitEnum.Gram));   
        AssertParse("1.1 Gram", new Quantity(1.1m, UnitEnum.Gram));   
    }

    private void AssertParse(string input, Quantity expected)
    {
        var result = QuantityParser.Parse(input);
        Assert.Equal(expected, result);  
    }
}