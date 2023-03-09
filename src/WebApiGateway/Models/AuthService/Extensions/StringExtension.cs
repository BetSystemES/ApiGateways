using System.IdentityModel.Tokens.Jwt;

namespace WebApiGateway.Models.AuthService.Extensions
{
    public static class StringExtension
    {
        public static JwtSecurityToken DecodeJwt(this string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(jwtToken);
            return jwtSecurityToken;
        }

        public static Dictionary<string, string> GetTokenClaims(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);
            Dictionary<string, string> dicClaims = jwtSecurityToken.Claims.ToDictionary(x => x.Type, x => x.Value);
            return dicClaims;
        }
    }
}