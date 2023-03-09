using System.Text.Json.Nodes;

namespace WebApiGateway.Models.API.Responses
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
