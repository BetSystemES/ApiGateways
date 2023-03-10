namespace WebApiGateway.Models.AuthService
{
    public class UserModel : AuthenticateModel
    {
        public bool IsLocked { get; set; }
    }
}
