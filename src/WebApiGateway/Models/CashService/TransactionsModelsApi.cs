namespace WebApiGateway.Models.CashService
{
    public class TransactionModelApi
    {
        public string? ProfileId { get; set; }
        public List<TransactionApi>? TransactionApis { get; set; }
    }
}