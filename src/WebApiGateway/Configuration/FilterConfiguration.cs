using WebApiGateway.Filters;

namespace WebApiGateway.Configuration
{
    public static class FilterConfiguration
    {
        public static IServiceCollection AddFilterConfiguration(this IServiceCollection services)
        {
            services.AddScoped<AuthFilter>();
            return services;
        }
    }
}