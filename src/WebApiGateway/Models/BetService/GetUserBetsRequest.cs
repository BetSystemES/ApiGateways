using WebApiGateway.Models.BaseModels;

namespace WebApiGateway.Models.BetService
{
    public class GetUserBetsRequest : BaseUserRequestModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
