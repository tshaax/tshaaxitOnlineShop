namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            // add carter module service.AddCarter()

            return services;
        }

        public static WebApplication UseAPIServices(this WebApplication app)
        {
            // app.UseMapster

            return app;
        }
    }
}
