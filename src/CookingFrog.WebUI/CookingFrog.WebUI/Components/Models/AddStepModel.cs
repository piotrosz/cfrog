namespace CookingFrog.WebUI.Components.Models;

public class AddStepModel(int? index, string step)
{
    public int? Index { get; } = index;

    public string Step { get; } = step;
}