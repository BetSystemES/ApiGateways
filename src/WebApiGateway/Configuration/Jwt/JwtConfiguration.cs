using WebApiGateway.AppDependencies;
using WebApiGateway.Settings;

namespace WebApiGateway.Configuration.Jwt
{
    public static class JwtConfiguration
    {
        public static WebApplicationBuilder JwtConfig(this WebApplicationBuilder builder)
        {
            builder.Services.ApplyClassFromConfig<JwtConfig>(builder.Configuration);
            return builder;
        }
    }
}