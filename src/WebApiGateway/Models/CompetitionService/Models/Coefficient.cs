﻿using WebApiGateway.Models.CompetitionService.Enums;

namespace WebApiGateway.Models.CompetitionService.Models
{
    public class Coefficient
    {
        public Guid Id { get; set; }

        public Guid CoefficientGroupId { get; set; }

        public CoefficientGroup CoefficientGroup { get; set; }

        public string Description { get; set; }

        public double Rate { get; set; }

        public CoefficientStatusType StatusType { get; set; }

        public double Amount { get; set; }

        public double Probability { get; set; }
    }
}
