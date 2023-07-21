using AutoMapper;
using CompetitionService.Grpc;
using Google.Protobuf.WellKnownTypes;
using WebApiGateway.Mapper.Extensions;
using WebApiGateway.Models.CompetitionService.UpdateModels;
using BusinessEntities = WebApiGateway.Models.CompetitionService.Entities;
using BusinessModels = WebApiGateway.Models.CompetitionService;
using BusnessEnums = WebApiGateway.Models.CompetitionService.Models.Enums;

namespace WebApiGateway.Mapper.CompetitionSerivce
{
    /// <summary>
    /// AutoMapper Profile for competition grpc serivce.
    /// </summary>
    /// <seealso cref="Profile" />
    public class CompetitionServiceProfile : Profile
    {
        public CompetitionServiceProfile()
        {
            CreateMap<string, Guid>()
                .ConvertUsing((x, res) => res = Guid.TryParse(x, out var id) ? id : Guid.Empty);
            CreateMap<Guid?, string>()
                .ConvertUsing((x, res) => res = x?.ToString() ?? string.Empty);

            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(x => Timestamp.FromDateTime(x)) ;
            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(x => x.ToDateTime());

            CreateMap<BusnessEnums.CoefficientGroupType, CoefficientGroupType>()
                .ReverseMap();
            CreateMap<BusnessEnums.CoefficientStatusType, CoefficientStatusType>()
                .ReverseMap();
            CreateMap<BusnessEnums.CoefficientOutcomeType, CoefficientOutcomeType>()
                .ReverseMap();
            CreateMap<BusnessEnums.CompetitionStatusType, CompetitionStatusType>()
                .ReverseMap();
            CreateMap<BusnessEnums.CompetitionType, CompetitionType>()
                .ReverseMap();

            CreateMap<BusinessEntities.Coefficient, Coefficient>()
                .ReverseMap();
            CreateMap<BusinessEntities.CoefficientGroup, CoefficientGroup>()
                .ReverseMap();
            CreateMap<BusinessEntities.CompetitionBase, CompetitionBase>()
                .ReverseMap();
            CreateMap<BusinessEntities.CompetitionDota2, CompetitionDota2>()
                .ReverseMap();

            CreateMap<CompetitionService.Grpc.CompetitionDota2UpdateModel, BusinessModels.UpdateModels.CompetitionDota2UpdateModel>()
                .ReverseMap();
            CreateMap<CompetitionService.Grpc.CoefficientUpdateModel, BusinessModels.UpdateModels.CoefficientUpdateModel>()
                .ReverseMap();
            CreateMap<CompetitionService.Grpc.CoefficientGroupUpdateModel, BusinessModels.UpdateModels.CoefficientGroupUpdateModel>()
                .ReverseMap();
            CreateMap<CompetitionService.Grpc.CompetitionBaseUpdateModel, BusinessModels.UpdateModels.CompetitionBaseUpdateModel>()
                .ReverseMap();

            CreateMap<BusinessModels.CreateModels.CoefficientCreateModel, Coefficient>()
                .Ignore(x => x.Id);

            CreateMap<BusinessModels.CreateModels.CoefficientGroupCreateModel, CoefficientGroup>()
                .Ignore(x => x.Coefficients)
                .Ignore(x => x.Id);

            CreateMap<BusinessModels.CreateModels.CompetitionBaseCreateModel, CompetitionBase>()
                .Ignore(x => x.CoefficientGroups)
                .Ignore(x => x.Id);

            CreateMap<BusinessModels.CreateModels.CompetitionDota2.CompetitionDota2CreateModel, CompetitionDota2>()
                .Ignore(x => x.CompetitionBase)
                .Ignore(x => x.Id);

            CreateMap<BusinessModels.CreateModels.CoefficientCreateModel, CoefficientCreateModel>()
                .ReverseMap();

            CreateMap<BusinessModels.CreateModels.CoefficientGroupCreateModel, CoefficientGroupCreateModel>()
                .ReverseMap();

            CreateMap<BusinessModels.CreateModels.CompetitionBaseCreateModel, CompetitionBaseCreateModel>()
                .ReverseMap();

            CreateMap<BusinessModels.CreateModels.CompetitionDota2.CompetitionDota2CreateModel, CompetitionDota2CreateModel>()
                .ReverseMap();
        }
    }
}