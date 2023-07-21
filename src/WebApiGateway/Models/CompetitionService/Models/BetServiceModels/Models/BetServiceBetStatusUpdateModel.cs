using WebApiGateway.Models.CompetitionService.Models.BetServiceModels.Enums;

namespace WebApiGateway.Models.CompetitionService.Models.BetServiceModels.Models
{
    /// <summary>
    /// Bet status update model.
    /// </summary>
    public class BetServiceBetStatusUpdateModel
    {
        /// <summary>
        /// Coefficient identifier.
        /// </summary>
        public Guid CoefficientId { get; set; }

        /// <summary>
        /// Bet status type.
        /// </summary>
        public BetServiceBetStatusType StatusType { get; set; }
    }
}
