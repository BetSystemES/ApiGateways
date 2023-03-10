namespace WebApiGateway.Models.API.Responses
{
    public class BasicApiResponse
    {
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }

        public BasicApiResponse(int statusCode, bool isSuccessful, string? message)
        {
            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }

}
