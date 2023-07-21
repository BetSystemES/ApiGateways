using WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Models.CompetitionService.UpdateModels
{
    /// <summary>
    /// Competition base entity
    /// </summary>
    public class CompetitionBaseUpdateModel
    {
        public CompetitionBaseUpdateModel()
        {
        }

        public Guid Id { get; set; }

        public CompetitionStatusType StatusType { get; set; }

        public CompetitionType Type { get; set; }

        public DateTime StartTime { get; set; }
    }
}
