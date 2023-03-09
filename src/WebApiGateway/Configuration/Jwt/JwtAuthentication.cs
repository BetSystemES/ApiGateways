using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiGateway.Settings;

namespace WebApiGateway.Configuration.Jwt
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
        {
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            return services;
        }
    }
}