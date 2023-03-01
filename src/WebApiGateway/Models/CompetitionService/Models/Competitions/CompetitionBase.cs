using WebApiGateway.Models.CompetitionService.Enums;

namespace WebApiGateway.Models.CompetitionService.Models.Competitions
{
    /// <summary>
    /// competition base entity
    /// </summary>
    public class CompetitionBase
    {
        public CompetitionBase()
        {
        }

        public Guid Id { get; set; }

        public CompetitionStatusType StatusType { get; set; }

        public CompetitionType Type { get; set; }

        public DateTime StartTime { get; set; }

        public List<CoefficientGroup> CoefficientGroups { get; set; }
    }
}
