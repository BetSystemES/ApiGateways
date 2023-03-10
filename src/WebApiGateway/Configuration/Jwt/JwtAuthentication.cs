using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
using WebApiGateway.Settings;

namespace WebApiGateway.Configuration.Jwt
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
        {
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            AuthRole authRole = AuthRole.Admin;
            var str = authRole.GetDescription();
            var str2 = AuthRole.Admin.GetDescription();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = jwtConfig.Issuer,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        //требуется ли наличие времени существования, default = true
                        RequireExpirationTime = true,
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