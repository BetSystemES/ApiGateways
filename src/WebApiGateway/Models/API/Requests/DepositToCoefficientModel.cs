using WebApiGateway.Models.BaseModels;

namespace WebApiGateway.Models.API.Requests
{
    public class DepositToCoefficientModel : BaseUserRequestModel
    {
        public string CoefficientId { get; set; }

        public double Amount { get; set; }
    }
}
