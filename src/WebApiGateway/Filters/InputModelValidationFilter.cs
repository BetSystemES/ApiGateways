using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Middleware;

namespace WebApiGateway.Filters
{
    public class InputModelValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new FilterException("The input is not valid");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }

   
}
