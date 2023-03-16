using WebApiGateway.Models.BaseModels;

namespace WebApiGateway.Models.CashService
{
    public class TransactionModelApi : BaseProfileRequstModel
    {
        public List<TransactionApi>? TransactionApis { get; set; }
    }
}