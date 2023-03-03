using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.SecurityTokenService;

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
                            // Here can be NotFoundException, ConflictException, FluentValidationException and etc.
                            case BadRequestException _:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                await context.Response.WriteAsJsonAsync(contextFeature.Error.Message);
                                break;
                        }
                    }

                });
            });
        }

    }
}