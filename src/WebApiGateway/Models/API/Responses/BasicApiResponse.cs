namespace WebApiGateway.Models.API.Responses
{
    public class BasicApiResponse<T> where T: class
    {
        public T? Data { get; set; }
        public Status Status { get; set; } = null!;
    }

    public class ApiResponse<T> : BasicApiResponse<T> where T: class 
    {
        public ApiResponse(T responceObject)
        {
            Data = responceObject;
            Status = new Status() { IsSuccessful = true };
        }
    }
}