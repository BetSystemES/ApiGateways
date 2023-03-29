namespace WebApiGateway.Models.BaseModels
{
    public class BasePagedResponseModel<T>
        where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}