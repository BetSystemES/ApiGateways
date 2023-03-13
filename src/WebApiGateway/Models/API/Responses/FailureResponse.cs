using System.Text.Json.Nodes;

namespace AuthService.Grpc.Interceptors.Helpers
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse(string reason, IEnumerable<IBaseExceptionDetails> details) : base(reason, details)
        {

        }
    }
}
