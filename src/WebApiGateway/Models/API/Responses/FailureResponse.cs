namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse(string reason, IEnumerable<IBaseExceptionDetail> details) : base(reason, details)
        {

        }
    }
}