using WebApiGateway.Models.CashService.Enums;

namespace WebApiGateway.Models.CashService
{
    public class TransactionApi
    {
        public string TransactionId { get; set; }
        public CashTypeApi CashType { get; set; }
        public double Amount { get; set; }
    }
}