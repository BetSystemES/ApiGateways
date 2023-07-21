using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.CreateModels
{
    /// <summary>
    /// Competition base entity
    /// </summary>
    public class CompetitionBaseCreateModel
    {
        public CompetitionStatusType StatusType { get; set; }

        public CompetitionType Type { get; set; }

        public DateTime StartTime { get; set; }
    }
}
