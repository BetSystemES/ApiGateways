namespace WebApiGateway.Settings
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public double TokenLifetimeInMinutes { get; set; }
        public double RefreshTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenSizeInBytes { get; set; }
    }
}