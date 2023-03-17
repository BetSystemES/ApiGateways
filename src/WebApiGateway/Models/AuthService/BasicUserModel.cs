using System.ComponentModel.DataAnnotations;

namespace WebApiGateway.Models.AuthService
{
    public class BasicUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}