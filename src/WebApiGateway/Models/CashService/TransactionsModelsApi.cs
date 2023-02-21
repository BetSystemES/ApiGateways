namespace WebApiGateway.Models.CashService
{
    public class TransactionModelApi
    {
        public string? Profileid { get; set; }
        public List<TransactionApi>? TransactionApis { get; set; }
    }

    public class TransactionApi
    {
        public string? Transactionid { get; set; }
        public CashTypeApi Cashtype { get; set; }
        public double Amount { get; set; }
    }

    public enum CashTypeApi
    {
        Unspecified = 0,
        Cash = 1,
        Bonus = 2,
    }
}
