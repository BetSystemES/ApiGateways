namespace WebApiGateway.Models.CompetitionService.CreateModels.CompetitionDota2
{
    public class CompetitionDota2CreateModel
    {
        public Guid CompetitionBaseId { get; set; }

        public Guid Team1Id { get; set; }

        public Guid Team2Id { get; set; }

        public string Team1Name { get; set; }

        public string Team2Name { get; set; }

        public int Team1KillAmount { get; set; }

        public int Team2KillAmount { get; set; }

        public DateTime TotalTime { get; set; }
    }
}
