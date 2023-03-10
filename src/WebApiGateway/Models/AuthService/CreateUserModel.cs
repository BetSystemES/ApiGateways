namespace WebApiGateway.Models.AuthService
{
    public class CreateUserModel : BasicUserModel
    {
        public List<string> Roles { get; set; }
        public CreateUserModel()
        {
            Roles = new List<string>();
        }
        public CreateUserModel(BasicUserModel basicUserModel)
        {
            Email = basicUserModel.Email;
            Password = basicUserModel.Password;
            Roles = new List<string>();
        }
    }
}