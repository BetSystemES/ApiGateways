using WebApiGateway.Filters;
using WebApiGateway.Services.Contracts;
using WebApiGateway.Services.Implementations;

namespace WebApiGateway.Configuration
{
    public static class FilterConfiguration
    {
        public static IServiceCollection AddFilterConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAuthClaimService, AuthClaimService>();
            services.AddScoped<AuthFilter>();
            return services;
        }
    }
}