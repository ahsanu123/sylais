using Microsoft.Extensions.DependencyInjection;
using Sylais.Steps;

namespace Sylais.Extensions;

public static class StepsExtension
{
    public static ServiceProvider RunStep<T>(this ServiceProvider serviceProvider)
        where T : IBaseStep
    {
        var captureAudioSteps = serviceProvider.GetRequiredService<T>();
        captureAudioSteps.Run();

        return serviceProvider;
    }
}
