namespace WebApiGateway.Models.AuthService
{
    public class CreateUserModel : AuthenticateModel
    {
        public List<string> Roles { get; set; }
    }
}