using FluentValidation;
namespace TestHub.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        return services;
    }
}