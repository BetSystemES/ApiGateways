using System.Security.Claims;

namespace WebApiGateway.Configuration.Jwt
{
    public static class AuthorizationPolicy
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                            context.User.HasClaim(ClaimTypes.Role, "Admin"))
                        .Build();
                });
            });

            return services;
        }
    }
}
