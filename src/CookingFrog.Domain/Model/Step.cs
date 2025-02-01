namespace CookingFrog.Domain;

public sealed record Step(string Description)
{
    public static implicit operator Step(string description) => new(description);
}