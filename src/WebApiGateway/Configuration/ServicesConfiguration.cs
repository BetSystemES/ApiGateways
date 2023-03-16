using WebApiGateway.Services.Contracts;
using WebApiGateway.Services.Implementations;

namespace WebApiGateway.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAuthClaimService, AuthClaimService>();
            return services;
        }
    }
}