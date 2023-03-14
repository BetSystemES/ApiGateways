using Newtonsoft.Json;

namespace WebApiGateway.Models.API.Responses
{
    public class StatusMessage
    {
        public bool IsSuccessful { get; set; }
        public string? Reason { get; set; }

        [JsonConverter(typeof(InterfaceConverter<IEnumerable<IBaseExceptionDetail>, List<GrpcExceptionDetail>>))]
        public IEnumerable<IBaseExceptionDetail> Details { get; set; }
    }
}