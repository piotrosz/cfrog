using CookingFrog.Domain.Parsing;
using FluentAssertions;

namespace CookingFrog.Domain.Tests;

public class RecipeParserTests
{
    [Fact]
    public void TestParseComplexRecipeSuccess()
    {
        var result = RecipeParser.Parse(
        "1:00",
        "🍲 Zupa z soczewicy",
        """
        2 spoon; oliwa
        1; cebula
        2 clove;czosnek
        1;marchewka
        3;ziemniaki
        50 g;czerwona soczewica
        750 ml;bulion drobiowy lub warzywny lub rosół
        1 teaspoon;kurkuma
        1 teaspoon;papryka w proszku
        Handful;ostra papryka
        1 can;krojone pomidory
        1 spoon; posiekany koperek
        0.3 glass; śmietanka  30% lub 18%
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
            1 szklanka;makaron orzo
            2 łyżki;oliwa
            1;mała cukinia
            1;czerwona papryka
            1;żółta papryka
            1;czerwona cebula
            1 ząbek; 🧄czosnek
            1 łyżka;biały ocet winny
            150 g;🍅pomidory koktajlowe
            ;bazylia
            100 g;feta  
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
    
    [Fact]
    public void TestParseComplexRecipeSuccess3()
    {
        var result = RecipeParser.Parse(
            "1:00",
            "🥘 Leczo",
            """
            2 łyżki;smalec
            2;cebula
            200 g;kiełbasy (np. wiejskiej, podsuszanej)
            2 ząbki;czosnek
            3;papryka (np. żółta, czerwona, zielona)
            2 łyżeczki;słodka papryka w proszku
            ;sól i świeżo zmielony pieprz
            0.5 łyżeczki;ostra papryka w proszku 
            500 ml; przecier pomidorowy - passata z butelki lub kartonu 
            """,
            """
            Do szerokiego garnka włożyć smalec, dodać pokrojoną w kosteczkę cebulę oraz pokrojoną na plasterki kiełbasę, smażyć co chwilę mieszając przez około 7 minut. Dodać starty na tarce lub rozgnieciony czosnek i smażyć jeszcze przez 3 minuty.
            Dodać pokrojone w kostkę papryki i co chwilę mieszając smażyć przez ok. 3 minuty. Doprawić solą (ok. pół łyżeczki), świeżo zmielonym pieprzem oraz słodką i ostrą papryką w proszku.
            Następnie dodać obrane i pokrojone w kostkę świeże pomidory (bez nasion ze środka komór) lub passatę pomidorową. Gotować przez około 15 minut pod uchyloną pokrywą, w międzyczasie kilka razy zamieszać. Na koniec dodać koncentrat pomidorowy jeśli używaliśmy świeżych pomidorów.
            """);
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void TestParseComplexRecipeSuccess4()
    {
        var result = RecipeParser.Parse(
            "1:00",
            "Chili con carne",
            """
            1 łyżka; oliwy
            1; cebula
            2 ząbki; czosnku
            1 łyżeczka; chilli w proszku
            1 łyżeczka; zmielonego kminu rzymskiego (kuminu)
            1 łyżeczka; suszonego oregano
            500 g ;mielonej wołowiny (np. gulaszowej, antrykotu)
            2 ;świeże pomidory lub 2/3 puszki obranych pomidorów
            1 łyżeczka; cukru
            1 ;czerwona papryka
            200 g ;czerwonej fasoli (ugotowanej)
            100 ml; bulionu np. wołowego lub wody
            100 g ;boczku (opcjonalnie) 
            5; kawałków suszonych pomidorów (opcjonalnie)
            """,
            """
            Na dużej patelni, na oliwie zeszklić pokrojoną w kosteczkę cebulę, dodać starty czosnek, chilli, kmin rzymski i oregano i mieszając smażyć przez 1 minutę. Stopniowo dodawać zmieloną wołowinę i zmielony lub drobno posiekany boczek, mieszając zrumieniać z każdej strony.
            Dodać pomidory (świeże należy sparzyć, obrać, pokroić na ćwiartki, usunąć pestki, miąższ pokroić w kosteczkę, pomidory z puszki należy rozdrobnić jeśli są w całości). Wymieszać i doprawić cukrem, pieprzem oraz szczyptą soli.
            Przykryć i dusić przez około 20 na umiarkowanym ogniu, co jakiś czas zamieszać. Dodać pokrojoną w kosteczkę paprykę i posiekane suszone pomidory jeśli ich używamy. Gotować pod przykryciem przez około 10 minut, od czasu do czasu zamieszać. Na koniec dodać fasolę i gotować pod przykryciem przez ok. 3 minuty.
            Na koniec gotować potrawę podlewając stopniowo wrzącym bulionem lub wodą, tak aby powstało trochę gęstego sosu. Można przygotować wcześniej i odgrzewać.
            """);
        
        result.IsSuccess.Should().BeTrue();
    }
}