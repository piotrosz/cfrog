using Microsoft.AspNetCore.Components.Web;

namespace CookingFrog.WebUI.Client.Components;

public partial class ComponentStateDemo
{
    private string _rendererLocation = string.Empty;
    private string _renderMode = string.Empty;

    protected override void OnInitialized()
    {
        _rendererLocation = RendererInfo.Name switch
        {
            "Static" => "Server (Static)",
            "Server" => "Server (Interactive)",
            "WebAssembly" => "Client (WebAssembly)",
            "WebView" => "Native (WebView)",
            _ => "Unknown"
        };

        _renderMode = AssignedRenderMode switch
        {
            null => "Static server",
            InteractiveServerRenderMode => "Interactive server",
            InteractiveWebAssemblyRenderMode => "Interactive WebAssembly",
            InteractiveAutoRenderMode => "Interactive Auto",
            _ => "Unknown"
        };
    }
}