namespace WebApiGateway.Responses
{
    public class BasicApiResponse
    {
        public int StatusCode { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }

        public BasicApiResponse(int statusCode, bool successful, string? message)
        {
            StatusCode = statusCode;
            Successful = successful;
            Message = message;
        }
    }

}
