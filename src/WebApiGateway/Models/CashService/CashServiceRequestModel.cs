using WebApiGateway.Models.Criterias;

namespace WebApiGateway.Models.CashService
{
    public class CashServiceRequestModel : FilterCriteriaRequestModel
    {
        public string? ProfileId { get; set; }

        public decimal? StartAmount { get; set; }

        public decimal? EndAmount { get; set; }
    }
}