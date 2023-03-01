using WebApiGateway.Models.CompetitionService.Enums;
using WebApiGateway.Models.CompetitionService.Models.Competitions;

namespace WebApiGateway.Models.CompetitionService.Models
{
    public class CoefficientGroup
    {
        public CoefficientGroup()
        {
        }

        public Guid Id { get; set; }

        public Guid CompetitionBaseId { get; set; }

        public CompetitionBase competitionBase { get; set; }

        public string Name { get; set; }

        public CoefficientGroupType Type { get; set; }

        public List<Coefficient> Coefficients { get; set; }
    }
}
