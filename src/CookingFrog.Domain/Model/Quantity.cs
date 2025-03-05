using System.Globalization;

namespace CookingFrog.Domain;

public sealed record Quantity(decimal Value, UnitEnum Unit)
{
    public static Quantity QuarterGlass => new(0.25m, UnitEnum.Glass);

    public static Quantity HalfTeaSpoon => new(0.5m, UnitEnum.Teaspoon);

    public static Quantity TeaSpoon => new(1m, UnitEnum.Teaspoon);
    
    public static Quantity One => new(1m, UnitEnum.Quantity);
    
    public static Quantity Half => new(0.5m, UnitEnum.Quantity);
    
    public static Quantity Undefined => new(0m, UnitEnum.Undefined);
    
    public static Quantity HalfKilo => new(0.5m, UnitEnum.Kilogram);
    
    public static Quantity Spoon => new(1m, UnitEnum.Spoon);
    
    public static Quantity Two => new(1m, UnitEnum.Quantity);
    
    public static Quantity HundredGram => new(100m, UnitEnum.Gram);
    
    public static Quantity TwoHundredGram => new(200m, UnitEnum.Gram);
    
    public static Quantity TwoHundredFiftyGram => new(250m, UnitEnum.Gram);
    
    public static Quantity ThreeHundredGram => new(300m, UnitEnum.Gram);
    
    public static Quantity Handful => new(1, UnitEnum.Handful);
    
    public static Quantity Quarter => new(0.25m, UnitEnum.Quantity);
    
    public static Quantity HalfSpoon => new(0.5m, UnitEnum.Spoon);

    public override string ToString()
    {
        return Unit switch
        {
            UnitEnum.Undefined when Value == 0 => string.Empty,
            UnitEnum.Quantity => Value.ToString(CultureInfo.InvariantCulture),
            _ => $"{Value} {Unit.GetDisplayDescription(Value)}"
        };
    }
}