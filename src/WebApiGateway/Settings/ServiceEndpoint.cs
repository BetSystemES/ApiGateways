namespace WebApiGateway.Settings
{
    public class ServiceEndpoint
    {
        public string? Name { get; set; }

        public string? Url { get; set; }

        public string? HealthCheckUrl { get; set; }
    }
}