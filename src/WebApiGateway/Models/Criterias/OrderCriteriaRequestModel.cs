using WebApiGateway.Models.Criterias.Enums;

namespace WebApiGateway.Models.Criterias
{
    public class OrderCriteriaRequestModel : PaginationCriteriaRequestModel
    {
        public string? ColumnName { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}