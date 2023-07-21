using WebApiGateway.Models.CashService.Enums;

namespace WebApiGateway.Models.CashService
{
    public class TransactionCreateModel
    {   
        public CashTypeApi CashType { get; set; }
        public double Amount { get; set; }
    }
}