using Refit;
using SpeechToTextApiClient.Data.DTOs;

namespace SpeechToTextApiClient.Data.DataSources;

internal interface IHealthCheckRemoteDataSource
{
    [Get("/healthcheck")]
    Task<HealthCheckDto> HealthCheckAsync();
}
