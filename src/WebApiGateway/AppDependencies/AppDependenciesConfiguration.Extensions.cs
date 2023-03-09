namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        public static void ApplyClassFromConfig<T>(this IServiceCollection services,
            IConfiguration configuration) where T : class
        {
            services.Configure<T>(configuration.GetSection(nameof(T)));
        }

        public static T GetSettings<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            return webApplicationBuilder.Configuration
                .GetRequiredSection(nameof(T))
                .Get<T>();
        }
    }
}