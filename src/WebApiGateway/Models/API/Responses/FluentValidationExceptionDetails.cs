namespace WebApiGateway.Models.API.Responses
{
    public class FluentValidationExceptionDetails : IBaseExceptionDetails
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
