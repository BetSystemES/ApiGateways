using System.Security.Claims;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
using static WebApiGateway.Models.Constants.PolicyConstants;

namespace WebApiGateway.Configuration.Jwt
{
    public static class AuthorizationPolicy
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            var claimRole = AuthRole.Admin;
            var claimNameValue = claimRole.GetDescription();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminPolicy, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                            context.User.HasClaim(ClaimTypes.Role, claimNameValue))
                        .Build();
                });
            });

            return services;
        }
    }
}