using Grpc.Core;
using static Grpc.Core.StatusCode;

namespace WebApiGateway.Middleware.Extensions;

public static class Extension
{
    public static int GetHttpCode(this StatusCode statusCode) =>
        statusCode switch
        {
            OK => 200,
            Cancelled => 499,
            Unknown => 500,
            InvalidArgument => 400,
            DeadlineExceeded => 504,
            NotFound => 404,
            AlreadyExists => 409,
            PermissionDenied => 403,
            Unauthenticated => 401,
            ResourceExhausted => 429,
            FailedPrecondition => 400,
            Aborted => 409,
            OutOfRange => 400,
            Unimplemented => 501,
            Internal => 500,
            Unavailable => 503,
            DataLoss => 500,
        };
}