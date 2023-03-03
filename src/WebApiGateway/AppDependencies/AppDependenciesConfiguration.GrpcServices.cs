using WebApiGateway.Settings;
using static ProfileService.GRPC.ProfileService;
using static CashService.GRPC.CashService;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static IServiceCollection AddGrpcClients(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings)
        {
            services
                .AddGrpcClient<ProfileServiceClient>(serviceEndpointsSettings)
                .AddGrpcClient<CashServiceClient>(serviceEndpointsSettings);

            return services;
        }

        private static IServiceCollection AddGrpcClient<T>(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings) where T : class
        {
            var serviceName = typeof(T).Name;

            var serviceEndpoint = serviceEndpointsSettings?.ServiceEndpoints
                .FirstOrDefault(x => x.Name.Equals(serviceName));

            ArgumentNullException.ThrowIfNull(serviceEndpoint, nameof(serviceEndpoint));

            return services
                .AddGrpcServiceClient<T>(serviceName, serviceEndpoint.Url);
        }

        private static IServiceCollection AddGrpcServiceClient<TClient>(this IServiceCollection services, string clientName, string endpoint) where TClient : class
        {
            return services
                .AddGrpcClient<TClient>(clientName, options =>
                {
                    options.Address = new Uri(endpoint);
                })
                .Services;
        }

    }
}
