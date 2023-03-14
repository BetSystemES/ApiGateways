namespace WebApiGateway.Models.API.Responses
{
    public class BasicApiResponse
    {
        public StatusMessage StatusMessage { get; set; } = null!;

        public BasicApiResponse()
        {
            StatusMessage = new StatusMessage()
            {
                IsSuccessful = true,
            };
        }

        public BasicApiResponse(string reason, IEnumerable<IBaseExceptionDetail> details)
        {
            StatusMessage = new StatusMessage()
            {
                IsSuccessful = false,
                Reason = reason,
                Details = details
            };
        }
    }
}