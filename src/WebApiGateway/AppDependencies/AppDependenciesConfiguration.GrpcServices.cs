using Grpc.Net.Client.Configuration;
using WebApiGateway.Services.Contracts;
using WebApiGateway.Settings;
using static AuthService.Grpc.AuthService;
using static CashService.GRPC.CashService;
using static ProfileService.GRPC.ProfileService;
using static WebApiGateway.Configuration.GrpcRetryPolicyConfiguration;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static IServiceCollection AddGrpcClients(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings)
        {
            services
                .AddGrpcClient<ProfileServiceClient>(serviceEndpointsSettings)
                .AddGrpcClient<CashServiceClient>(serviceEndpointsSettings)
                .AddGrpcClient<AuthServiceClient>(serviceEndpointsSettings);
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
                .AddGrpcServiceClient<T>(serviceName, serviceEndpoint.Url)
                .AddCallCredentialsToClient(serviceEndpointsSettings.IsUseTokenTransfer);
        }

        private static IHttpClientBuilder AddGrpcServiceClient<TClient>(this IServiceCollection services,
            string clientName, string endpoint) where TClient : class
        {
            return services
                .AddGrpcClient<TClient>(clientName, options =>
                {
                    options.Address = new Uri(endpoint);
                    options.ChannelOptionsActions.Add(channelOptions =>
                    {
                        channelOptions.ServiceConfig = new ServiceConfig { MethodConfigs = { DefaultMethodConfig } };
                        channelOptions.UnsafeUseInsecureChannelCallCredentials = true;
                    });
                });
        }

        private static IServiceCollection AddCallCredentialsToClient(this IHttpClientBuilder clientBuilder, bool isUseTokenTransfer)
        {
            if (isUseTokenTransfer)
            {
                clientBuilder
                    .AddCallCredentials((context, metadata, serviceProvider) =>
                    {
                        var provider = serviceProvider.GetRequiredService<IAuthClaimService>();
                        var token = provider.GetToken();
                        metadata.Add("Authorization", $"{token}");
                        return Task.CompletedTask;
                    });
            }
            return clientBuilder.Services;
        }
    }
}