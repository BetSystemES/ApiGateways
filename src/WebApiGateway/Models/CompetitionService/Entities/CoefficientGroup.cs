using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.Entities
{
    public class CoefficientGroup
    {
        public CoefficientGroup()
        {
        }

        public Guid Id { get; set; }

        public Guid CompetitionBaseId { get; set; }

        public string Name { get; set; }

        public CoefficientGroupType Type { get; set; }

        public List<Coefficient> Coefficients { get; set; }
    }
}
