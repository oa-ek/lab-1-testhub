using Application.repositories.interfaces;
using Application.repositories.interfaces.common;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestHub.Infrastructure.data.repositories;
using TestHub.Infrastructure.data.repositories.common;

namespace TestHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<TestHubDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}