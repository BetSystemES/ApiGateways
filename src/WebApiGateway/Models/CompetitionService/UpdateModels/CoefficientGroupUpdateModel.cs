using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.UpdateModels
{
    public class CoefficientGroupUpdateModel
    {
        public CoefficientGroupUpdateModel()
        {
        }

        public Guid Id { get; set; }

        public Guid CompetitionBaseId { get; set; }

        public string Name { get; set; }

        public CoefficientGroupType Type { get; set; }
    }
}
