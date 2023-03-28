namespace WebApiGateway.Models.Criterias
{
    public class FilterCriteriaRequestModel : OrderCriteriaRequestModel
    {
        public List<Guid>? UserIds { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? SearchCriteria { get; set; }
    }
}