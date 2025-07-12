using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Sylais.Commands;
using Sylais.Constant;
using Sylais.Steps;

namespace Sylais.Extensions;

public static class ServiceCollectionExtension
{
    public static ServiceCollection RegisterServices(this ServiceCollection services)
    {
        services.AddSingleton<PiperServerCommand>();
        services.AddSingleton<WhisperCppCommand>();

        services.AddSingleton<CaptureAudioSteps>();
        services.AddSingleton<PlayAudioSteps>();
        services.AddSingleton<TranscribeAudioSteps>();

        services.AddHttpClient(
            HttpConstant.JsonContentTypeClient,
            client =>
            {
                client.DefaultRequestHeaders.Add(HeaderNames.ContentType, "application/json");
            }
        );

        return services;
    }
}
