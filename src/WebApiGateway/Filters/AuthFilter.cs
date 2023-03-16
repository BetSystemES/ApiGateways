using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Services.Contracts;

namespace WebApiGateway.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        private readonly IAuthClaimService _authClaimService;
        public AuthFilter(IAuthClaimService authClaimService)
        {
            _authClaimService = authClaimService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? requestId = null;
            //get model from context
            foreach (var value in context.ActionArguments.Values)
            {
                switch (value)
                {
                    case BaseUserRequestModel request:
                        requestId = request.UserId.ToString();
                        break;
                    case BaseProfileRequstModel request:
                        requestId = request.ProfileId;
                        break;
                    default: return;
                }
            }

            if (!string.IsNullOrEmpty(requestId))
            {
                var authenticatedUserId = _authClaimService.AuthenticatedUserId();
                var authRoles = _authClaimService.GetRoles();

                if (AccessFilter(authRoles, authenticatedUserId.ToString(), requestId))
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }
        }

        public bool AccessFilter(IEnumerable<AuthRole> authRoles, string authenticatedUserId, string requestId)
        {
            //Если роль админ, то мы не выкидываем 403
            if (authRoles.Contains(AuthRole.Admin))
            {
                return false;
            }
            //Если не админ, и идшники не совпали, то выкидываем 403
            return (!authenticatedUserId.Equals(requestId));

            //Simple User

            //a) (+) Throw forbidden if AuthenticatedUserId != profileId(in all its manifestations) from routes or bodies(all services except auth_service)

            //a) (+) Throw forbidden if AuthenticatedUserId != user_id(in all its manifestations) from routes or bodies(in auth_service)

            //b) (-) Do not throw forbidden if AuthenticatedUserId == profileId(in all its manifestations) from routes or bodies(all services except auth_service)

            //c) (-) Do not throw forbidden if AuthenticatedUserId == user_id(in all its manifestations) from routes or bodies(in auth_service)

            //Admin User

            //a) (-) Do not throw forbidden for all cases
        }
    }
}