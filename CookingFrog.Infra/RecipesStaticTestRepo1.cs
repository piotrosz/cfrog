using CookingFrog.Domain;

namespace CookingFrog.Infra;

public sealed partial class RecipesStaticTestRepo
{
    private static readonly IReadOnlyList<Recipe> Recipes =
    [
        Recipe.Create(
            "🦃 Kotlety mielone",
            TimeSpan.FromHours(1.5),
            [
                new Ingredient("🥖 sucha bułka", Quantity.One),
                new Ingredient("🥛 mleko lub woda do namoczenia bułki", Quantity.Undefined),
                new Ingredient("🦃 mięso mielone z indyka", Quantity.HalfKilo),
                new Ingredient("🧅 średnia cebula starta", Quantity.One),
                new Ingredient("🥚 jajko", Quantity.One),
                new Ingredient("🧂 sól", Quantity.TeaSpoon),
                new Ingredient("czarny pieprz zmielony", Quantity.HalfTeaSpoon),
                new Ingredient("zimna woda", Quantity.QuarterGlass),
                new Ingredient("bułka tarta", Quantity.Undefined),
                new Ingredient("smalec lub masło klarowane lub olej", Quantity.Undefined)
            ],
            [
                "Bułkę zalać mlekiem lub wodą, odstawić do namoczenia na około 10 - 15 minut. ",
                "Do większej miski włożyć zmielone mięso, startą na drobnej tarce cebulę, jajko, sól i pieprz oraz odciśniętą bułkę, wszystko dobrze wymieszać.",
                "W trakcie wyrabiania mięsa należy dodawać po troszku zimnej wody i wyrabiać tak długo aż mięso wchłonie wodę i nie będzie przywierać do dłoni. Im dłużej wyrabiamy, tym lepsze kotlety. Masa mięsna może wydawać się dość luźna, ale dzięki temu kotlety będą delikatniejsze, mniej zbite i twarde.",
                "Uformować podłużne kotlety, obtoczyć w bułce tartej i kłaść na dobrze rozgrzany olej na patelni. Po obsmażeniu z dwóch stron przełożyć kotlety do garnka lub naczynia żaroodpornego (bez przykrycia) i wstawić do rozgrzanego do 150 stopni C piekarnika, na około 15 minut. Unikniemy długiego smażenia, a kotlety będą w środku idealnie miękkie."

            ]),
        Recipe.Create(
            "🥗 Sałatka batat, avocado, jajko",
            TimeSpan.FromHours(1.0),
            [
                new Ingredient("🥔 batat", Quantity.One),
                new Ingredient("🧄 ząbek czosnku", Quantity.Half),
                new Ingredient("sól", Quantity.Undefined),
                new Ingredient("pieprz", Quantity.Undefined),
                new Ingredient("🌶 słodka papryka", Quantity.HalfTeaSpoon),
                new Ingredient("oliwa", Quantity.Spoon),
                new Ingredient("🥚 jajko", Quantity.Two),
                new Ingredient("🥑 awokado", Quantity.One),
                new Ingredient("🍋 sok z limonki lub cytryny", Quantity.TeaSpoon),
                new Ingredient("pomidorki koktajlowe", Quantity.HundredGram),
                new Ingredient("szpinak", Quantity.Handful),
                new Ingredient("czerwona cebula", Quantity.Quarter),
                new Ingredient("sok z limonki lub cytryny", Quantity.Spoon, "sos"),
                new Ingredient("oliwa ekstra", Quantity.Spoon, "sos"),
                new Ingredient("musztarda miodowa", Quantity.TeaSpoon, "sos"),
                new Ingredient("sól, pieprz", Quantity.Handful, "sos"),
                new Ingredient("opcjonalnie majonez", Quantity.TeaSpoon, "sos"),
                new Ingredient("opcjonalnie płatki chilli", Quantity.Handful, "sos"),

            ], steps:
            [
                "Piekarnik nagrzać do 210C. Batata obrać, umyć, pokroić na kawałki, doprawić startym czosnkiem, solą, pieprzem i papryką w proszku. Wymieszać z oliwą i ułożyć na blaszce do pieczenia lub w naczyniu żaroodpornym i piec przez ok. 20 - 25 minut (do miękkości).",
                "Jajka ugotować na twardo (6 minut od zagotowania się wody), obrać i przekroić na połówki.",
                "Awokado przekroić na pół, wyjąć pestkę, obrać, pokroić na plasterki, skropić sokiem z cytryny.",
                "Pomidorki umyć i przekroić na połówki. Cebulę obrać i przekroić na pół i pokroić na cienkie plasterki.",
                "Do misek lub pojemników na lunch włożyć szpinak, jajka, awokado, pomidorki, cebulę. Doprawić solą i pieprzem. Dodać upieczone bataty.",
                "Przygotować sos mieszając wszystkie składniki."
            ]),
        Recipe.Create("🍜 Makaron w sosie pomidorowo-czosnkowym z kurczakiem",
            TimeSpan.FromMinutes(30),
            [
                new Ingredient("mięso pierś kurczaka", Quantity.ThreeHundredGram),
                new Ingredient("makaron Mie", Quantity.TwoHundredFiftyGram),
                new Ingredient("warzywa mrożone, azja", Quantity.ThreeHundredGram),
                new Ingredient("miód", new Quantity(50, UnitEnum.Gram)),
                new Ingredient("sos sojowy ciemny", new Quantity(50, UnitEnum.Millilitre)),
                new Ingredient("keczup", new Quantity(50, UnitEnum.Gram)),
                new Ingredient("mąka", new Quantity(25, UnitEnum.Gram)),
                new Ingredient("woda", new Quantity(80, UnitEnum.Millilitre)),
                new Ingredient("szczypiorek", new Quantity(20, UnitEnum.Gram)),
                new Ingredient("olej sezamowy", new Quantity(20, UnitEnum.Gram)),
                new Ingredient("przyprawa do kurczaka", new Quantity(4, UnitEnum.Gram)),
                new Ingredient("czosnek granulowany", new Quantity(8, UnitEnum.Gram)),
            ], steps:
            [
                "Dopraw mięso w misce przyprawami (czosnek granulowany, przyp do kurczaka), obtocz mięso w mące, dodaj 1/4 oleju sezamowego.",
                "smaż 🐤 na patelni z 1/4 oleju sezamowego przez 6-8min. Ugotuj makaron. Przelej 80ml wody z makaronu do szklanki. Makaron odcedź i przelej zimną wodą.",
                "do dużej miski wlej sos sojowy, ketchup, miód, czosnek granulowany, wodę z makaronu, olej sezamowy i całość wymieszaj",
                "Smaż warzywa mrożone 8-10min. Dodaj 🐤, makaron i sos. Dokładnie mieszaj i gotuj 2-3min",
                "Wyłóż porcje na talerz. Posyp posiekanym szczypiorem."
            ]),
        Recipe.Create("🥗 Sałatka makaronowa awokado, feta, pomidory, kukurydza",
            TimeSpan.FromHours(0.5),
            [
                new Ingredient("makaron kokardki", Quantity.HundredGram),
                new Ingredient("ser feta", new Quantity(150, UnitEnum.Gram)),
                new Ingredient("🥑 awokado", Quantity.One),
                new Ingredient("🍋 limonka", Quantity.Half),
                new Ingredient("🌽 kukurydza w puszce", Quantity.One),
                new Ingredient("🍅 pomidory koktajlowe", Quantity.TwoHundredGram),
                new Ingredient("🧅 czerwona cebula", Quantity.Half),
                new Ingredient("sól, pieprz", Quantity.Handful),
                new Ingredient("🌶 ostra papryka", Quantity.Handful),
                new Ingredient("liście bazylii", new Quantity(0.5m, UnitEnum.Glass)),
                new Ingredient("oliwa", Quantity.TeaSpoon),
                new Ingredient("majonez", Quantity.TeaSpoon)
            ], steps:
            [
                "Makaron ugotować w osolonej wodzie, odcedzić i wsypać do salaterki.",
                "Dodać pokrojony w kosteczkę ser, a także obrane i pokrojone w kosteczkę awokado. Skropić sokiem z limonki.",
                "Dodać odcedzoną kukurydzę, pokrojone na połówki pomidorki, posiekaną cebulę i całość doprawić solą, pieprzem i papryką.",
                "Dodać posiekane listki bazylii, oliwę oraz majonez. Wszystko delikatnie wymieszać."
            ]),
        Recipe.Create("🥞 Placki ziemniaczane",
            TimeSpan.FromHours(1),
            [
                new Ingredient("ziemniaki", Quantity.HalfKilo),
                new Ingredient("mąka pszenna", Quantity.HalfSpoon),
                new Ingredient("cebula", Quantity.Quarter),
                new Ingredient("jajko", Quantity.One),
                new Ingredient("sól"),
                new Ingredient("olej roślinny do smażenia")
            ], [
                "Ziemniaki obrać i zetrzeć na tarce o małych oczkach bezpośrednio do większej i płaskiej miski. Zostawić je w misce bez mieszania, miskę delikatnie przechylić i odstawić tak na ok. 5 minut.",
                "W międzyczasie odlewać zbierający się sok, delikatnie przytrzymując ziemniaki, nadal ich nie mieszać. Na koniec docisnąć dłonią do miski i odlać jeszcze więcej soku. Dodać mąkę, drobno startą cebulę, jajko oraz dwie szczypty soli.",
                "Rozgrzać patelnię, wlać olej. Masę ziemniaczaną wymieszać. Nakładać porcje masy (1 pełna łyżka) na rozgrzany olej i rozprowadzać ją na dość cienki placek. Smażyć na średnim ogniu przez ok. 2 - 3 minuty na złoty kolor, przewrócić na drugą stronę i powtórzyć smażenie.",
                "Odkładać na talerz wyłożony ręcznikami papierowymi. Posypać solą morską z młynka. Placki ziemniaczane najlepsze są prosto z patelni gdy są chrupiące."
            ])
    ];
}
