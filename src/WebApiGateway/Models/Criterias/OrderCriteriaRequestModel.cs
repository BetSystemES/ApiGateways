using OrderDirection = ProfileService.GRPC.OrderDirection;

namespace WebApiGateway.Models.Criterias
{
    public class OrderCriteriaRequestModel : PaginationCriteriaRequestModel
    {
        public string? ColumnName { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}