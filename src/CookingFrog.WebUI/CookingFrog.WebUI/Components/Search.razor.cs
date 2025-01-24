using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CookingFrog.WebUI.Components;

public partial class Search
{
    private string SearchTerm { get; set; } = string.Empty;

    [Parameter] 
    public EventCallback<string> SearchCalled { get; set; }
    
    private async Task DoSearch(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await SearchCalled.InvokeAsync(SearchTerm);
        }
    }
}