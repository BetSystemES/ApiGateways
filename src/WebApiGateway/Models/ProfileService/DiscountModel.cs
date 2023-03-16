using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.ProfileService.Enums;

namespace WebApiGateway.Models.ProfileService
{
    public class DiscountModel : BaseProfileRequstModel
    {
        public string? Id { get; set; }
        public bool IsAlreadyUsed { get; set; }
        public DiscountModelType Type { get; set; }
        public double Amount { get; set; }
        public double DiscountValue { get; set; }
    }
}