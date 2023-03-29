using WebApiGateway.Models.Criterias;

namespace WebApiGateway.Models.BonusService
{
    public class BonusServiceRequestModel : FilterCriteriaRequestModel
    {
        public string? ProfileId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}