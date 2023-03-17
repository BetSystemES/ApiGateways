using WebApiGateway.Mapper.AuthService;
using WebApiGateway.Mapper.CashService;
using WebApiGateway.Mapper.ProfileService;

namespace WebApiGateway.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<ProfileModelMap>();
                config.AddProfile<DiscountModelMap>();
                config.AddProfile<TransactionModelApiMap>();
                config.AddProfile<AuthModelMap>();
            });

            return services;
        }
    }
}