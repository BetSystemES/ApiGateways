namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse(string reason, IEnumerable<IBaseExceptionDetail> details) : base(reason, details)
        {
        }

        public FailureResponse(string reason, IEnumerable<GrpcExceptionDetail> details) : base(reason, details)
        {
        }
    }
}