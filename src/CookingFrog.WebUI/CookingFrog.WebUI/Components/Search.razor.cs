using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class Search
{
    private string? _searchTerm { get; set; }

    [Parameter] public EventCallback<string> SearchCalled { get; set; }
}