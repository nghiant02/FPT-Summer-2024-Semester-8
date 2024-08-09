using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

public static class HttpClientConfiguration
{
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("eStoreClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5145/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

        return services;
    }
}
