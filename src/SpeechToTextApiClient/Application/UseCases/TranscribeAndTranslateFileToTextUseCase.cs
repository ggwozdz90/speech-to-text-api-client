using Microsoft.Extensions.Logging;
using SpeechToTextApiClient.Domain.Services;

namespace SpeechToTextApiClient.Application.UseCases;

internal interface ITranscribeAndTranslateFileToTextUseCase
{
    Task<string> InvokeAsync(string filePath, string sourceLanguage, string targetLanguage);
}

internal sealed class TranscribeAndTranslateFileToTextUseCase(
    ILogger<TranscribeAndTranslateFileToTextUseCase> logger,
    ITranscribeService transcribeService
) : ITranscribeAndTranslateFileToTextUseCase
{
    public async Task<string> InvokeAsync(string filePath, string sourceLanguage, string targetLanguage)
    {
        logger.LogTrace(
            "Transcribing and translating file {FilePath} from {SourceLanguage} to {TargetLanguage} invoked from use case...",
            filePath,
            sourceLanguage,
            targetLanguage
        );

        var result = await transcribeService
            .TranscribeAndTranslateAsync(filePath, sourceLanguage, targetLanguage)
            .ConfigureAwait(false);

        return result;
    }
}
