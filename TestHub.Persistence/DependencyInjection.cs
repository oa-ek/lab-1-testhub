using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestHub.Persistence.repositories;

namespace TestHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<TestHubDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ITestRepository, TestRepository>();

        return services;
    }
}