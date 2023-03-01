namespace WebApiGateway.Models.CashService
{
    // TODO: Create multiple files for all classes and enums. Each class and enum should have own file
    public class TransactionModelApi
    {
        // TODO: typo in Profileid. Should be ProfileId
        public string? Profileid { get; set; }
        public List<TransactionApi>? TransactionApis { get; set; }
    }

    // TODO: Create multiple files for all classes and enums. Each class and enum should have own file
    public class TransactionApi
    {
        // TODO: typo in Transactionid. Should be TransactionId
        public string? Transactionid { get; set; }
        // TODO: typo in Cashtype. Should be CashType
        public CashTypeApi Cashtype { get; set; }
        public double Amount { get; set; }
    }

    // TODO: Create new folder in Models "Enums" and place CashTypeApi enum there
    public enum CashTypeApi
    {
        Unspecified = 0,
        Cash = 1,
        Bonus = 2,
    }
}
