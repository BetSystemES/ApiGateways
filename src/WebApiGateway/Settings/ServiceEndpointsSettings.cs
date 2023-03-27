namespace WebApiGateway.Settings
{
    /// <summary>
    /// Service endpoints
    /// </summary>
    public class ServiceEndpointsSettings
    {
        public List<ServiceEndpoint> ServiceEndpoints { get; set; }
        public bool IsUseTokenTransfer { get; set; }
    }
}