using Application.contracts.infrastructure;
using Application.models;
using Application.models.identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestHub.Infrastructure.mail;
using TestHub.Infrastructure.services;

namespace TestHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}