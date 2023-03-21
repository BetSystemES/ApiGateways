using System.ComponentModel.DataAnnotations;

namespace WebApiGateway.Models.AuthService
{
    public class BasicTokenModel
    {
        [Required]
        public string AuthToken { get; set; }
    }
}