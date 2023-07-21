namespace WebApiGateway.Models.CompetitionService.Requests
{
    public class CreateCompetitionDota2RequestModel<T> where T : class
    {
        public T CompetitionModel { get; set; }
    }
}
