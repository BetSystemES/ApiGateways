namespace WebApiGateway.Models.FilterCriteria
{
    public class FilterCriteriaRequestModel : OrderCriteriaRequestModel
    {
        public List<Guid>? UserIds { get; set; }
        public bool IsEnabled { get; set; }
        public string? SearchCriteria { get; set; }
    }
}