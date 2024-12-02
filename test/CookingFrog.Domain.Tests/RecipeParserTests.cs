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

        var recipe = result.Result;

        recipe.Summary.Should().Be("Nazwa");
        recipe.Ingredients.Should().HaveCount(3);
        recipe.Steps.Should().HaveCount(2);
    }
}