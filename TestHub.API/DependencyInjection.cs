using TestHub.Infrastructure.file;

namespace TestHub.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddScoped<ILocalFileStorage, LocalFileStorage>();
        services.AddScoped<ICloudFileStorage, CloudFileStorage>();

        return services;
    }
}