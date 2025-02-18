# Speech-to-Text API Client

This library provides a client for interacting with the Speech-to-Text API. It offers a simple and efficient interface to handle API endpoints, allowing you to easily send requests and receive responses for further processing.

## Features

- **Transcription**: Transcribe audio files using the Speech-to-Text API.
- **Translation**: Transcribe and translate audio files to a specified language.
- **Health Check**: Check the health status of the Speech-to-Text API.
- **Dependency Injection**: Easily register the library's services in the .NET dependency injection container.

## Interface

The library provides an interface for interacting with the Speech-to-Text API through the `ISpeechToTextAdapter` interface. The interface includes the following methods:

- `Task<string> TranscribeAsync(string filePath, string sourceLanguage)`: Transcribes the audio from the specified file.
- `Task<string> TranscribeAndTranslateAsync(string filePath, string sourceLanguage, string targetLanguage)`: Transcribes and translates the audio from the specified file.
- `Task<string> HealthCheckAsync()`: Checks the health of the API.

## Dependency Injection

You can register the library's services in the .NET dependency injection container using the `AddSpeechToTextProcessor` extension method. This method registers all necessary services and configurations.

### Example

```csharp
public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddSpeechToTextProcessor(configuration);
}
```

## Available Distributions

The library is available on GitHub Packages. You can install it using the following commands:

```shell
dotnet nuget add source --username YOUR_GITHUB_USERNAME --password YOUR_GITHUB_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/ggwozdz90/index.json"

dotnet add package SpeechToTextApiClient
```

## API Project

The API project is available on [GitHub](https://github.com/ggwozdz90/speech-to-text-api) and as a Docker image on [Docker Hub](https://hub.docker.com/r/ggwozdz/speech-to-text-api).

## Usage

### Transcription

```csharp
var adapter = serviceProvider.GetRequiredService<ISpeechToTextAdapter>();
var result = await adapter.TranscribeAsync("path/to/audio/file.wav", "en_US");
Console.WriteLine(result);
```

### Transcription and Translation

```csharp
var adapter = serviceProvider.GetRequiredService<ISpeechToTextAdapter>();
var result = await adapter.TranscribeAndTranslateAsync("path/to/audio/file.wav", "en_US", "es_ES");
Console.WriteLine(result);
```

### Health Check

```csharp
var adapter = serviceProvider.GetRequiredService<ISpeechToTextAdapter>();
var result = await adapter.HealthCheckAsync();
Console.WriteLine(result);
```

## Configuration

The library uses the .NET configuration system. You can configure the base address of the Speech-to-Text API, as well as the default source and target languages, and the timeout settings for HTTP clients in your `appsettings.json` file:

```json
{
  "SpeechToText": {
    "BaseAddress": "http://localhost:8000",
    "SourceLanguage": "en_US",
    "TargetLanguage": "pl_PL",
    "TranscribeRouteTimeout": 300,
    "HealthCheckRouteTimeout": 10
  }
}
```

## License

This project is licensed under the MIT License - see the [LICENSE](../LICENCE) file for details.

## Table of Contents

- [Speech-to-Text API Client](#speech-to-text-api-client)
  - [Features](#features)
  - [Interface](#interface)
  - [Dependency Injection](#dependency-injection)
    - [Example](#example)
  - [Available Distributions](#available-distributions)
  - [API Project](#api-project)
  - [Usage](#usage)
    - [Transcription](#transcription)
    - [Transcription and Translation](#transcription-and-translation)
    - [Health Check](#health-check)
  - [Configuration](#configuration)
  - [License](#license)
  - [Table of Contents](#table-of-contents)
