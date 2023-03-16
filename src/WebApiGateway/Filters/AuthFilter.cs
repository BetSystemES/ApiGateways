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

                if (IsExecutionAllowed(authRoles, authenticatedUserId.ToString(), requestId))
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }
        }

        public bool IsExecutionAllowed(IEnumerable<AuthRole> authRoles, string authenticatedUserId, string requestId)
        {
            //If the role is admin, then we do not throw out 403
            if (authRoles.Contains(AuthRole.Admin))
            {
                return false;
            }
            //If the role is not admin, and the IDs do not match, then we throw out 403
            return (!authenticatedUserId.Equals(requestId));
        }
    }
}