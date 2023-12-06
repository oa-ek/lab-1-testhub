using Application.contracts.infrastructure.email;
using Application.contracts.infrastructure.file;
using Microsoft.Extensions.DependencyInjection;
using TestHub.Infrastructure.services.file;

namespace TestHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}