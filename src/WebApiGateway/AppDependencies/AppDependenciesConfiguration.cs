namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        public static void ConfigureDependencies(this WebApplicationBuilder builder)
        {
            var appSettings = builder.AddServiceEndpoints();
            // TODO: remove unused variable
            var configuration = builder.Configuration;

            builder
                .Services
                .AddHttpContextAccessor()
                .AddSwagger()
                .AddGrpcClients(appSettings);
        }
    }
}
