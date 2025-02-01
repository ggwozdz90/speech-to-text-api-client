using System.IO.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using SpeechToTextApiClient.Adapter.Adapters;
using SpeechToTextApiClient.Application.UseCases;
using SpeechToTextApiClient.Data.DataSources;
using SpeechToTextApiClient.DependencyInjection;
using SpeechToTextApiClient.Domain.Repositories;
using SpeechToTextApiClient.Domain.Services;

namespace SpeechToTextApiClient.Tests.DependencyInjection;

[TestFixture]
internal sealed class ServiceCollectionExtensionsTests
{
    private IServiceCollection services = null!;
    private IConfiguration configuration = null!;

    [SetUp]
    public void Setup()
    {
        configuration = Substitute.For<IConfiguration>();
        var configurationSection = Substitute.For<IConfigurationSection>();
        configurationSection.Value.Returns("http://localhost:5000");
        configuration.GetSection("SpeechToText:BaseAddress").Returns(configurationSection);
        services = new ServiceCollection();
        services.AddSingleton(configuration);
    }

    [Test]
    public void AddSpeechToTextProcessor_ShouldRegisterFileSystem()
    {
        // Given, When
        services.AddSpeechToTextProcessor(configuration);
        using var serviceProvider = services.BuildServiceProvider();

        // Then
        serviceProvider.GetService<IFileSystem>().Should().NotBeNull();
    }

    [Test]
    public void AddSpeechToTextProcessor_ShouldRegisterDataLayer()
    {
        // Given, When
        services.AddSpeechToTextProcessor(configuration);
        using var serviceProvider = services.BuildServiceProvider();

        // Then
        serviceProvider.GetService<IFileAccessLocalDataSource>().Should().NotBeNull();
        serviceProvider.GetService<ITranscribeRemoteDataSource>().Should().NotBeNull();
        serviceProvider.GetService<IHealthCheckRemoteDataSource>().Should().NotBeNull();
        serviceProvider.GetService<ISpeechToTextRepository>().Should().NotBeNull();
    }

    [Test]
    public void AddSpeechToTextProcessor_ShouldRegisterDomainLayer()
    {
        // Given, When
        services.AddSpeechToTextProcessor(configuration);
        using var serviceProvider = services.BuildServiceProvider();

        // Then
        serviceProvider.GetService<ITranscribeService>().Should().NotBeNull();
        serviceProvider.GetService<IHealthCheckService>().Should().NotBeNull();
    }

    [Test]
    public void AddSpeechToTextProcessor_ShouldRegisterApplicationLayer()
    {
        // Given, When
        services.AddSpeechToTextProcessor(configuration);
        using var serviceProvider = services.BuildServiceProvider();

        // Then
        serviceProvider.GetService<ITranscribeFileToTextUseCase>().Should().NotBeNull();
        serviceProvider.GetService<ITranscribeAndTranslateFileToTextUseCase>().Should().NotBeNull();
        serviceProvider.GetService<IHealthCheckUseCase>().Should().NotBeNull();
    }

    [Test]
    public void AddSpeechToTextProcessor_ShouldRegisterAdapterLayer()
    {
        // Given, When
        services.AddSpeechToTextProcessor(configuration);
        using var serviceProvider = services.BuildServiceProvider();

        // Then
        serviceProvider.GetService<ISpeechToTextAdapter>().Should().NotBeNull();
    }
}
