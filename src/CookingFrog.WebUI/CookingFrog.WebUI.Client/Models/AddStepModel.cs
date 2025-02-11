namespace CookingFrog.WebUI.Client.Models;

public record AddStepModel(int? Index, string Step)
{
    public int? Index { get; } = Index;

    public string Step { get; } = Step;
}