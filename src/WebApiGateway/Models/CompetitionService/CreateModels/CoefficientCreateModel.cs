using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.CreateModels
{
    public class CoefficientCreateModel
    {
        public Guid CoefficientGroupId { get; set; }

        public string Description { get; set; }

        public double Rate { get; set; }

        public CoefficientStatusType StatusType { get; set; }

        public double Amount { get; set; }

        public double Probability { get; set; }

        public CoefficientOutcomeType OutcomeType { get; set; }
    }
}
