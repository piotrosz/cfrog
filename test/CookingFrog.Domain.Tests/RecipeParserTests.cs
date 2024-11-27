using CookingFrog.Domain.Parsing;
using FluentAssertions;

namespace CookingFrog.Domain.Tests;

public class RecipeParserTests
{
    [Fact]
    public void TestParseSuccess()
    {
        var result = RecipeParser.Parse(
            "1:30", 
            "Nazwa",
            ["cos;5 gram", "jakaś rzecz ;1 teaspoon", "sól ;1 handful"],
            ["zrób to", "zrób tamto"]);

        result.Summary.Should().Be("Nazwa");
        result.Ingredients.Should().HaveCount(3);
        result.Steps.Should().HaveCount(2);
    }
}