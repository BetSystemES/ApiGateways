using Microsoft.AspNetCore.Mvc.Filters;

using WebApiGateway.Middleware;
using static WebApiGateway.Filters.Support;

namespace WebApiGateway.Filters
{
    public class InputNullValidationFilter2 : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(kv => IsAnyNullOrEmpty(kv.Value)))
            {
                var responseObj = new ExceptionObject()
                {
                    StatusCode = 400,
                    Successful = false,
                    Error = "Arguments cannot be null",
                };

                throw new FilterException(responseObj);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
