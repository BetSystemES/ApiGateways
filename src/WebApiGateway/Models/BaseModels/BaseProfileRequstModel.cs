using System.ComponentModel.DataAnnotations;

namespace WebApiGateway.Models.BaseModels
{
    public class BaseProfileRequstModel
    {
        [Required] public string ProfileId { get; set; }
    }
}
