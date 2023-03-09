using WebApiGateway.Settings;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static ServiceEndpointsSettings AddServiceEndpoints(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Host.ConfigureAppConfiguration(config =>
            {
                var prefix = "Gateway_";
                config.AddEnvironmentVariables(prefix);
                config.Build();
            });

            webApplicationBuilder.Services.ApplyClassFromConfig<ServiceEndpointsSettings>(webApplicationBuilder.Configuration);
            
            return webApplicationBuilder.GetSettings<ServiceEndpointsSettings>();
        }
    }
}