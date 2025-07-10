using Microsoft.Extensions.DependencyInjection;
using Sylais.Commands;
using Sylais.Steps;

namespace Sylais.Extensions;

public static class ServiceCollectionExtension
{
    public static ServiceCollection RegisterServices(this ServiceCollection services)
    {
        services.AddSingleton<PiperCommand>();
        services.AddSingleton<WhisperCppCommand>();

        services.AddSingleton<CaptureAudioSteps>();
        services.AddSingleton<PlayAudioSteps>();
        services.AddSingleton<TranscribeAudioSteps>();

        return services;
    }
}
