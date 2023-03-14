namespace WebApiGateway.Models.API.Responses
{
    public class GrpcExceptionDetail : IBaseExceptionDetail
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public GrpcExceptionDetail(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}