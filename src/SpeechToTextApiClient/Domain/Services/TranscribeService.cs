using Microsoft.Extensions.Logging;
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
        logger.LogTrace(
            "Transcribing file {FilePath} from {SourceLanguage} invoked from service...",
            filePath,
            sourceLanguage
        );

        try
        {
            return await speechToTextRepository.TranscribeAsync(filePath, sourceLanguage).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is NetworkException or TranscribeException or FileAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occurred while transcribing the file {FilePath} from {SourceLanguage} from service...",
                filePath,
                sourceLanguage
            );
            throw;
        }
    }

    public async Task<string> TranscribeAndTranslateAsync(string filePath, string sourceLanguage, string targetLanguage)
    {
        logger.LogTrace(
            "Transcribing and translating file {FilePath} from {SourceLanguage} to {TargetLanguage} invoked from service...",
            filePath,
            sourceLanguage,
            targetLanguage
        );

        try
        {
            return await speechToTextRepository
                .TranscribeAndTranslateAsync(filePath, sourceLanguage, targetLanguage)
                .ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is NetworkException or TranscribeException or FileAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occurred while transcribing and translating the file {FilePath} from {SourceLanguage} to {TargetLanguage} from service...",
                filePath,
                sourceLanguage,
                targetLanguage
            );
            throw;
        }
    }
}
