using System.ComponentModel.DataAnnotations;

namespace WebApiGateway.Models.AuthService
{
    public class CreateUserModel : BasicUserModel
    {
        [MinLength(1)]
        public IEnumerable<string> RoleIds { get; set; }

        public CreateUserModel()
        {
            RoleIds = new List<string>();
        }

        public CreateUserModel(BasicUserModel basicUserModel)
        {
            Email = basicUserModel.Email;
            Password = basicUserModel.Password;
            RoleIds = new List<string>();
        }
    }
}