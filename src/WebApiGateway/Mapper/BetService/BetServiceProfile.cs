using AutoMapper;
using BetService.Grpc;
using Google.Protobuf.WellKnownTypes;
using BusinessEnums = WebApiGateway.Models.BetService.Enums;
using BusinessModels = WebApiGateway.Models.BetService.Entities;

namespace WebApiGateway.Mapper.BetService
{
    /// <summary>Profile of grpc layer</summary>
    public class BetServiceProfile : Profile
    {
        public BetServiceProfile()
        {
            CreateMap<BusinessModels.BetViewModel, Bet>()
                .ForMember(x => x.StatusType, y => y.MapFrom(z => z.BetStatusType))
                .ForMember(x => x.PayoutType, y => y.MapFrom(z => z.PayoutStatus))
                .ReverseMap();
            CreateMap<BetCreateModel, BusinessModels.BetViewModel>();
            CreateMap<BusinessEnums.BetPayoutStatus, BetPayoutStatus>()
                .ReverseMap();
            CreateMap<BusinessEnums.BetStatusType, BetStatusType>()
                .ReverseMap();
        }
    }
}
