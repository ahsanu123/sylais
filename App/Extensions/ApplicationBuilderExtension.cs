using Microsoft.Extensions.DependencyInjection;
using Sylais.Repositories;

namespace Sylais.Extensions;

public static class ApplicationBuilderExtension
{
    public static IServiceCollection AddSylaisServices(this IServiceCollection services)
    {
        services.AddSingleton<AnotherRepository>();
        services.AddSingleton<BaseRepository>();

        return services;
    }
}
