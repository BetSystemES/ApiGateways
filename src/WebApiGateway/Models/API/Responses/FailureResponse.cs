using System.Text.Json.Nodes;

namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse(string reason, List<IBaseExceptionDetails> details) : base(reason, details)
        {
            
        }
    }
}
