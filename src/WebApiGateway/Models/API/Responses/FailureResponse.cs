namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse(string reason, IEnumerable<IBaseExceptionDetails> details) : base(reason, details)
        {

        }
    }
}
