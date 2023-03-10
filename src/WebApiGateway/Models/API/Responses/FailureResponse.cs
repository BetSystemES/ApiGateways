using System.Text.Json.Nodes;

namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse<T> : BasicApiResponse<T> where T : class
    {
        public FailureResponse(string reason, Exception error)
        {
            Data = null;
            Status = new Status()
            {
                IsSuccessful = false,
                Reason = reason,
            };
            Status.Messages = new List<ErrorMessage>()
            {
                new ErrorMessage()
                {
                    Field = "",
                    Message = error.Message,
                }
            };
        }
    }
}
