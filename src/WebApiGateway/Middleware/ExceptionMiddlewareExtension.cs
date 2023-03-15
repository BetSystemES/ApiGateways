using System.Net;
using Grpc.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.SecurityTokenService;
using Newtonsoft.Json;
using WebApiGateway.Middleware.Extensisons;
using WebApiGateway.Models.API.Responses;

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

                            case RpcException exception:
                                context.Response.StatusCode = exception.StatusCode.GetHttpCode();
                                try
                                {
                                    string statusDetail = exception.Status.Detail;
                                    var statusMessage = JsonConvert.DeserializeObject<StatusMessage>(statusDetail);

                                    FailureResponse failureResponse =
                                        new FailureResponse(statusMessage?.Reason, statusMessage?.Details);

                                    await context.Response.WriteAsJsonAsync(failureResponse);
                                }
                                catch
                                {
                                    await context.Response.WriteAsJsonAsync("GRPC Exception");
                                }
                                break;
                        }
                    }
                });
            });
        }
    }
}