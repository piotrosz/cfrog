using CookingFrog.Domain.Parsing;
using FluentAssertions;

namespace CookingFrog.Domain.Tests;

public class RecipeParserTests
{
    [Fact]
    public void TestParseSuccessSimple()
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

    [Fact]
    public void TestParseComplexRecipeSuccess()
    {
        var result = RecipeParser.Parse(
        "1:00",
        "🍲 Zupa z soczewicy",
        """
        oliwa;2 spoon
        cebula;1
        czosnek;2 clove
        marchewka;1
        ziemniaki;3
        czerwona soczewica;50 g
        bulion drobiowy lub warzywny lub rosół; 750 ml
        kurkuma; 1 teaspoon 
        papryka w proszku; 1 teaspoon
        ostra papryka; Handful
        krojone pomidory; 1 can
        posiekany koperek; 1 spoon
        śmietanka  30% lub 18%; 0.3 glass
""",
         """
         W garnku na oliwie zeszklić pokrojoną w kosteczkę cebulę. 
         Dodać przeciśnięty przez praskę czosnek oraz marchewkę - obraną i startą na dużych oczkach tartki.
         Następnie wrzucić obrane i pokrojone w kostkę ziemniaki, mieszając co chwilę podsmażać przez ok. 3 minuty.
         Dodać suchą soczewicę i wymieszać. Wlać bulion i zagotować. Dodać kurkumę, słodką i ostrą paprykę oraz świeżo zmielony pieprz i sól do smaku (w razie potrzeby). 
         Gotować pod przykryciem przez ok. 15 - 20 minut.
         Dodać krojone pomidory z puszki i gotować przez ok. 15 minut pod uchyloną pokrywą, od czasu do czasu zamieszać.
         Odstawić z ognia, wymieszać z posiekanym koperkiem oraz śmietanką.
""");
        
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void TestParseComplexRecipeSuccess2()
    {
        var result = RecipeParser.Parse(
            "1:00",
            "🥗 Sałatka na ciepło z makaronem orzo, warzywami oraz fetą",
            """
            makaron orzo;1 szklanka
            oliwa; 2 łyżki
            mała cukinia; 1
            czerwona papryka; 1
            żółta papryka; 1
            czerwona cebula; 1
            🧄czosnek;1 ząbek
            biały ocet winny; 1 łyżka
            🍅pomidory koktajlowe; 150 g
            bazylia;
            feta; 100 g 
            """,
            """
            Makaron wsypać na osolony wrzątek i gotować al dente, przez ok. 12 minut. Odcedzić, włożyć z powrotem do garnka, wymieszać z 1 łyżką oliwy.
            W międzyczasie na dużą patelnię włożyć pokrojone w kostkę warzywa: cukinię, paprykę czerwoną oraz żółtą, czerwoną cebulę. Doprawić solą, pieprzem oraz skropić 1 łyżką oliwy i smażyć, aż będą miękkie i lekko zrumienione, przez ok. 5 minut. Pod koniec dodać przeciśnięty przez praskę czosnek i chwilę razem podsmażyć.
            Na koniec skropić warzywa octem winnym i potrząsnąć patelnią w celu wymieszania składników.
            Odstawić patelnię z ognia, dodać makaron orzo i wymieszać. Posypać pokrojonymi na połówki pomidorkami oraz listkami bazylii.
            Wyłożyć do talerzu lub jednej salaterki i posypać pokrojoną fetą lub serem sałatkowym.
            """);
        
        result.IsSuccess.Should().BeTrue();
    }
}