using System.ComponentModel.DataAnnotations;
using WebApiGateway.Models.BaseModels;

namespace WebApiGateway.Models.AuthService
{
    public class DeleteUserModel : BaseUserRequestModel
    {
        [Required] public string Email { get; set; }
    }
}