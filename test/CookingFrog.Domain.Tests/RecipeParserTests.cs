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
            "cos;5 gram\n jakaś rzecz ;1 teaspoon\nsól ;1 handful",
            "zrób to\nzrób tamto");

        result.Summary.Should().Be("Nazwa");
        result.Ingredients.Should().HaveCount(3);
        result.Steps.Should().HaveCount(2);
    }
}