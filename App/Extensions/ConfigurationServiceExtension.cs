using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sylais.Commands;
using Sylais.Models;

namespace Sylais.Extensions;

public static class ConfigurationCollectionExtension
{
    public static ServiceCollection RegisterConfigurationServices(this ServiceCollection services)
    {
        services.AddSingleton<IConfigurationRoot>(serviceProvider =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
        );

        services.AddSingletonConfig<AudioFileConfig>();
        services.AddSingletonConfig<PiperConfig>();

        services.AddSingleton<WhisperCppCommand>();

        return services;
    }

    public static string GetTempWavPath(this AudioFileConfig audioConfig)
    {
        var tempWavPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            audioConfig.FolderName,
            audioConfig.FileName
        );
        return Path.GetFullPath(tempWavPath);
    }

    public static void AddSingletonConfig<T>(this ServiceCollection services)
        where T : class
    {
        services.AddSingleton<T>(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfigurationRoot>();

            if (configuration == null)
                throw new Exception($"Cant find configuration service");

            var configValue = configuration.GetSection(typeof(T).Name).Get<T>();
            if (configValue == null)
                throw new Exception($"Cant Find {typeof(T).Name} In Configuration");

            return configValue;
        });
    }
}
