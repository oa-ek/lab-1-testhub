using Application.contracts.infrastructure.file;
using Application.contracts.persistence.authentication;
using TestHub.Infrastructure.services.authentication;
using TestHub.Infrastructure.services.file;
using TestHub.Infrastructure.services.mail;

namespace TestHub.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.AddScoped<ILocalFileStorage, LocalFileStorage>();
        services.AddScoped<ICloudFileStorage, CloudFileStorage>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}