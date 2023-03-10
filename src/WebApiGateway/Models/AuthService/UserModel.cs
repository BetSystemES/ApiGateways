namespace WebApiGateway.Models.AuthService
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
    }
}
