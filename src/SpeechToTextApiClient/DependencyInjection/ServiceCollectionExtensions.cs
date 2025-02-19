using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SpeechToTextApiClient.Adapter.Adapters;
using SpeechToTextApiClient.Application.UseCases;
using SpeechToTextApiClient.Data.DataSources;
using SpeechToTextApiClient.Data.Repositories;
using SpeechToTextApiClient.Domain.Repositories;
using SpeechToTextApiClient.Domain.Services;

namespace SpeechToTextApiClient.DependencyInjection;

/// <summary>
///     This class is responsible for registering internal services in the external container in the application that will
///     use this library.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the SpeechToTextAdapter service to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSpeechToTextProcessor(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IFileSystem, FileSystem>();
        services.AddDataLayer(configuration);
        services.AddDomainLayer();
        services.AddApplicationLayer();
        services.AddAdapterLayer();

        return services;
    }

    private static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBaseAddress = configuration.GetValue("SpeechToText:BaseAddress", "http://localhost:8000");
        var transcribeRouteTimeout = configuration.GetValue("SpeechToText:TranscribeRouteTimeout", 300);
        var healthCheckRouteTimeout = configuration.GetValue("SpeechToText:HealthCheckRouteTimeout", 10);

        services.AddTransient<IFileAccessLocalDataSource, FileAccessLocalDataSource>();

        services
            .AddRefitClient<ITranscribeRemoteDataSource>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(apiBaseAddress);
                c.Timeout = TimeSpan.FromSeconds(transcribeRouteTimeout);
            });

        services
            .AddRefitClient<IHealthCheckRemoteDataSource>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(apiBaseAddress);
                c.Timeout = TimeSpan.FromSeconds(healthCheckRouteTimeout);
            });

        services.AddTransient<ISpeechToTextRepository, SpeechToTextRepository>();
    }

    private static void AddDomainLayer(this IServiceCollection services)
    {
        services.AddTransient<ITranscribeService, TranscribeService>();
        services.AddTransient<IHealthCheckService, HealthCheckService>();
    }

    private static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<ITranscribeFileToTextUseCase, TranscribeFileToTextUseCase>();
        services.AddTransient<ITranscribeAndTranslateFileToTextUseCase, TranscribeAndTranslateFileToTextUseCase>();
        services.AddTransient<IHealthCheckUseCase, HealthCheckUseCase>();
    }

    private static void AddAdapterLayer(this IServiceCollection services)
    {
        services.AddTransient<ISpeechToTextAdapter, SpeechToTextAdapter>();
    }
}
