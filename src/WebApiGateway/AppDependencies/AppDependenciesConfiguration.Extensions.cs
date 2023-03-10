namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        public static void ApplyClassFromConfig<T>(this IServiceCollection services,
            IConfiguration configuration) where T : class
        {
            services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }

        public static T GetAppSettings<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            return webApplicationBuilder.Configuration
                .GetRequiredSection(typeof(T).Name)
                .Get<T>();
        }
    }
}