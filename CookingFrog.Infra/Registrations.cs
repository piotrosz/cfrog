using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CookingFrog.Infra;

public static class Registrations
{
    public static void AddFrogStorage(this IServiceCollection services, Uri serviceUri)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddTableServiceClient(serviceUri);
            clientBuilder.UseCredential(new DefaultAzureCredential());
        });
    }
}