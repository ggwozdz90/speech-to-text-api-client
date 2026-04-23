using Microsoft.Extensions.Logging;
using System.NET.Http;
using SpeechToTextApiClient.Domain.Exceptions;
using SpeechToTextApiClient.Domain.Repositories;

namespace SpeechToTextApiClient.Domain.Services;

internal interface ITranscribeService
{
    Task<string> TranscribeAsync(string filePath, string sourceLanguage);
    Task<string> TranscribeAndTranslateAsync(string filePath, string sourceLanguage, string targetLanguage);
}

internal sealed class TranscribeService(
    ILogger<TranscribeService> logger,
    ISpeechToTextRepository speechToTextRepository
) : ITranscribeService
{
    public async Task<string> TranscribeAsync(string filePath, string sourceLanguage)
    {
        // TODO: implement logic
    }

    public async Task<string> TranscribeAndTranslateAsync(string filePath, string sourceLanguage, string targetLanguage)
    {
        // TODO: implement logic
    }
}
