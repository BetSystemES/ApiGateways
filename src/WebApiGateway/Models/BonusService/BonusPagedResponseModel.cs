using WebApiGateway.Models.ProfileService;

namespace WebApiGateway.Models.BonusService
{
    public class BonusPagedResponseModel
    {
        public int TotalCount { get; set; }
        public List<DiscountModel> Data { get; set; }
    }
}
