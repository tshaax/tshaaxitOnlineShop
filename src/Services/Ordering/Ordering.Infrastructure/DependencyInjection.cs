

using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrucureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add sql db services

            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Add service to the container
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

           services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
