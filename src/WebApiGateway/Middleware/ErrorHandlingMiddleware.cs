using Microsoft.AspNetCore.Mvc;

namespace WebApiGateway.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (FilterException filterException)
            {
                context.Response.StatusCode = filterException.ExceptionObject.StatusCode;
                await context.Response.WriteAsJsonAsync(filterException.ExceptionObject);
            }
           
        }
    }
}
