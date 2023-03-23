using WebApiGateway.Models.FilterCriteria.Enums;

using OrderDirection = ProfileService.GRPC.OrderDirection;

namespace WebApiGateway.Models.FilterCriteria
{
    public class OrderCriteriaRequestModel : PaginationCriteriaRequestModel
    {
        public string? ColumnName { get; set; }
        public OrderDirection OrderDirection { get; set; }
    }
}