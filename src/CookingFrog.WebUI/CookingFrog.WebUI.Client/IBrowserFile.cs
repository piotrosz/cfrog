using Microsoft.AspNetCore.Components.Forms;

namespace CookingFrog.WebUI.Client;

// This is just a redeclaration of the IBrowserFile interface from Microsoft.AspNetCore.Components.Forms
// to ensure our code compiles correctly
public interface IBrowserFile
{
    string Name { get; }
    string ContentType { get; }
    long Size { get; }
    DateTimeOffset LastModified { get; }
    Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default);
}
