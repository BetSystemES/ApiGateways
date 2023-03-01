// TODO: remove all unnecessary empty lines (make file clean and pretty)
namespace WebApiGateway.Settings
{
    // TODO: Create multiple files for all classes and enums. Each class and enum should have own file
    /// <summary>
    /// Service endpoints
    /// </summary>
    public class ServiceEndpointsSettings
    {
        public List<ServiceEndpoint> ServiceEndpoints { get; set; }
    }

    // TODO: Create multiple files for all classes and enums. Each class and enum should have own file
    public class ServiceEndpoint
    {
        public string? Name { get; set; }

        public string? Url { get; set; }

        public string? HealthCheckUrl { get; set; }
    }


}
