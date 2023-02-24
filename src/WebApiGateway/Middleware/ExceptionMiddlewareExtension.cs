using Microsoft.AspNetCore.Diagnostics;

namespace WebApiGateway.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        switch (contextFeature.Error)
                        {
                            case FilterException filterException:

                                context.Response.StatusCode = filterException.ExceptionObject.StatusCode;
                                await context.Response.WriteAsJsonAsync(filterException.ExceptionObject);
                                break;
                        }
                    }

                });
            });
        }

    }
}