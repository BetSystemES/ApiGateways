using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static WebApiGateway.Filters.Support;


namespace WebApiGateway.Filters
{
    public class InputNullValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (
                context.ActionArguments.Any(kv => IsAnyNullOrEmpty(kv.Value)))
            {
                var responseObj = new
                {
                    successful = false,
                    error = "Arguments cannot be null",
                };

                // setting the result shortcuts the pipeline, so the action is never executed
                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
