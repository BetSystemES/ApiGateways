using WebApiGateway.Settings;
using static ProfileService.GRPC.Profiler;
using static CashService.GRPC.Casher;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static IServiceCollection AddGrpcClients(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings)
        {
            services
                .AddGrpcClient<ProfilerClient>(serviceEndpointsSettings)
                .AddGrpcClient<CasherClient>(serviceEndpointsSettings);

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
                .AddGrcpServiceClient<T>(serviceName, serviceEndpoint.Url);
        }

        // TODO: typo in AddGrcpServiceClient (Grcp should be replaced with Grpc)
        private static IServiceCollection AddGrcpServiceClient<TClient>(this IServiceCollection services, string clientName, string endpoint) where TClient : class
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
