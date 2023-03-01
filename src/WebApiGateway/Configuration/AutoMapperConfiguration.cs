using WebApiGateway.Mapper.CashService;
using WebApiGateway.Mapper.ProfileService;

namespace WebApiGateway.Configuration
{
    // TODO: remove partial
    public static partial class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<ProfileModelMap>();
                config.AddProfile<DiscountModelMap>();
                config.AddProfile<TransactionModelApiMap>();
            });

            return services;
        }

    }
}
