namespace WebApiGateway.Models.API.Responses
{
    public class StatusMessage
    {
        public bool IsSuccessful { get; set; }
        public string? Reason { get; set; }
        public List<IBaseExceptionDetails> Details { get; set; }
    }
}
