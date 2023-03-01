namespace WebApiGateway.Models.ProfileService
{
    // TODO: Create multiple files for all classes and enums. Each class and enum should have own file
    public class DiscountModel
    {
        public string? Id { get; set; }
        // TODO: typo in Personalid. Should be PersonalId
        public string? Personalid { get; set; }
        // TODO: typo in Isalreadyused. Should be IsAlreadyUsed
        public bool Isalreadyused { get; set; }
        public DiscountModelType Type { get; set; }
        public double Amount { get; set; }
        // TODO: typo in Discountvalue. Should be DiscountValue
        public double Discountvalue { get; set; }
    }

    // TODO: Create new folder in Models "Enums" and place DiscountModelType enum there
    public enum DiscountModelType
    {
        Unspecified = 0,
        Amount = 1,
        Discount = 2,
    }
}
