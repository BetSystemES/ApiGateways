using WebApiGateway.Mapper.AuthService;
using WebApiGateway.Mapper.BetService;
using WebApiGateway.Mapper.CashService;
using WebApiGateway.Mapper.CompetitionSerivce;
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
                config.AddProfile<AuthProfile>();
                config.AddProfile<CompetitionServiceProfile>();
                config.AddProfile<BetServiceProfile>();
            });

            return services;
        }
    }
}