using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sylais.Commands;
using Sylais.Models;

namespace Sylais.Extensions;

public static class ServiceCollectionExtension
{
    public static ServiceCollection RegisterServices(this ServiceCollection services)
    {
        services.AddSingleton<IConfigurationRoot>(serviceProvider =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
        );

        services.AddSingletonConfig<DependenciesBinaryPathConfig>();

        services.AddSingleton<PiperCommand>();
        services.AddSingleton<WhisperCppCommand>();

        return services;
    }

    public static void AddSingletonConfig<T>(this ServiceCollection services)
        where T : class
    {
        services.AddSingleton<T>(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfigurationRoot>();

            if (configuration == null)
                throw new Exception($"Cant find configuration service");

            var configValue = configuration.GetSection(nameof(T)).Get<T>();
            if (configValue == null)
                throw new Exception($"Cant Find {nameof(T)} In Configuration");

            return configValue;
        });
    }
}
