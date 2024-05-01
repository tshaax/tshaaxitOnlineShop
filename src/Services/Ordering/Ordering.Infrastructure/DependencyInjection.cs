using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrucureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add sql db services

            var connectionString = configuration.GetConnectionString("Database");

            return services;
        }
    }
}
