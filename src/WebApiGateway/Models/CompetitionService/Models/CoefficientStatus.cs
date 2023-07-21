using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.Models
{
    public class CoefficientStatus
    {
        public Guid CoefficientId { get; set; }

        public CoefficientOutcomeType OutcomeType { get; set; }
    }
}
