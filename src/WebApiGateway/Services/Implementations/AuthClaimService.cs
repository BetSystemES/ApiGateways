using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using WebApiGateway.Configuration.Jwt;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
using WebApiGateway.Services.Contracts;

namespace WebApiGateway.Services.Implementations
{
    public class AuthClaimService : IAuthClaimService
    {
        private readonly IEnumerable<AuthRole> _roles;
        private readonly string _token;
        private readonly Guid _userId;

        public AuthClaimService(IHttpContextAccessor contextAccessor)
        {
            ArgumentNullException.ThrowIfNull(contextAccessor, nameof(contextAccessor));

            if (contextAccessor.HttpContext != null)
            {
                _roles = contextAccessor.HttpContext.User?
                    .FindFirst(ClaimTypes.Role)?.Value.Split(',')
                        .Select(x => x.GetEnumItem<AuthRole>());

                _token = contextAccessor.HttpContext?.Request?.Headers[HeaderNames.Authorization];

                var userId = contextAccessor.HttpContext.User.FindFirstValue(JwtAuthConstants.DefaultIdFieldName);

                if (Guid.TryParse(userId, out Guid guid))
                {
                    _userId = guid;
                }
            }
        }

        public IEnumerable<AuthRole> GetRoles() => _roles;

        public string GetToken() => _token;

        public Guid AuthenticatedUserId() => _userId;
    }
}