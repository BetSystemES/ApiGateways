using Microsoft.AspNetCore.Mvc.Filters;

using WebApiGateway.Middleware;
using static WebApiGateway.Filters.Support;

namespace WebApiGateway.Filters
{
    public class InputNullValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(kv => IsAnyNullOrEmpty(kv.Value)))
            {
                throw new FilterException("Arguments cannot be null");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
