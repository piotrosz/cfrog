namespace CookingFrog.Domain;

public sealed class Step(string description)
{
    public string Description { get; } = description;
    
    public static implicit operator Step(string description) => new Step(description);
}