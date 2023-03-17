using System.ComponentModel.DataAnnotations;

namespace WebApiGateway.Models.BaseModels
{
    public class BaseUserRequestModel
    {
        [Required] public Guid UserId { get; set; }
    }
}