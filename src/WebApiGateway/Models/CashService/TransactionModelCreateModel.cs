using WebApiGateway.Models.BaseModels;

namespace WebApiGateway.Models.CashService
{
    public class TransactionModelCreateModel : BaseProfileRequstModel
    {
        public List<TransactionCreateModel>? TransactionApis { get; set; }
    }
}