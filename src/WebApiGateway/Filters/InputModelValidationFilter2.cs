using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Middleware;

namespace WebApiGateway.Filters
{
    public class InputModelValidationFilter2 : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var responseObj = new ExceptionObject()
                {
                    StatusCode = 400,
                    Successful = false,
                    Error = "The input is not valid",
                };

                throw new FilterException(responseObj);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }

   
}
