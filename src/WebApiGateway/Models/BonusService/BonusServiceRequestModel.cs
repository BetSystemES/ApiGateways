using WebApiGateway.Models.FilterCriteria;

namespace WebApiGateway.Models.BonusService
{
    public class BonusServiceRequestModel : FilterCriteriaRequestModel
    {
        public string? ProfileId { get; set; }
    }
}
