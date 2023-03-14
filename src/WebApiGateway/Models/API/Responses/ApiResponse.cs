namespace WebApiGateway.Models.API.Responses
{
    public class ApiResponse<T> : BasicApiResponse where T : class
    {
        public T? Data { get; set; }

        public ApiResponse()
        {
            Data = null;
        }

        public ApiResponse(T responceObject)
        {
            Data = responceObject;
        }
    }
}