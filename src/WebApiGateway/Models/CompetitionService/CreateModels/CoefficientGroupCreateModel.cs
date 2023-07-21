using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.CreateModels
{
    public class CoefficientGroupCreateModel
    {
        public Guid CompetitionBaseId { get; set; }

        public string Name { get; set; }

        public CoefficientGroupType Type { get; set; }
    }
}
