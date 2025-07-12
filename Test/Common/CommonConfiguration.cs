using Microsoft.Extensions.Configuration;

namespace Sylais.Test.Common;

public static class CommonConfiguration
{
    public static IConfiguration InitConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    public static T? GetConfigValue<T>(IConfiguration configuration)
    {
        return configuration.GetSection(typeof(T).Name).Get<T>();
    }
}
