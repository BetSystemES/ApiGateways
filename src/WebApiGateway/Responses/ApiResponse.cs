using System.Text.Json.Nodes;

namespace WebApiGateway.Responses
{
    public class ApiResponse : BasicApiResponse
    {
        public JsonObject ResponceObject { get; set; }

        public ApiResponse(int statusCode, string? message, JsonObject responceObject) : base(statusCode, true, message)
        {
            ResponceObject = responceObject;
        }
    }
}
