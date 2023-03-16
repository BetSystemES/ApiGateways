using WebApiGateway.Models.AuthService.Enums;

namespace WebApiGateway.Services.Contracts
{
    public interface IAuthClaimService
    {
        IEnumerable<AuthRole> GetRoles();
        string GetToken();
        Guid AuthenticatedUserId();
    }
}