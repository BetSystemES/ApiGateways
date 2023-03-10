namespace WebApiGateway.Models.API.Responses
{
    public class Status
    {
        public bool IsSuccessful { get; set; }
        public string? Reason { get; set; }
        public List<ErrorMessage>? Messages { get; set; }
    }
}
