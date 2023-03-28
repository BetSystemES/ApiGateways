using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using ProfileService.GRPC;
using WebApiGateway.Models.ProfileService;
using WebApiGateway.Models.ProfileService.Enums;

namespace WebApiGateway.Mapper.ProfileService
{
    public class DiscountModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscountModelMap"/> class.
        /// </summary>
        public DiscountModelMap()
        {
            CreateMap<DiscountModelType, DiscountType>()
                .ReverseMap();

            CreateMap<Discount, DiscountModel>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.ProfileId)))
                .ForMember(dest => dest.CreateDate,
                    opt =>
                        opt.MapFrom(src => src.CreateDate.ToDateTimeOffset()))
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.Type));

            CreateMap<DiscountModel, Discount>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CreateDate,
                    opt =>
                        opt.MapFrom(src => Timestamp.FromDateTimeOffset(src.CreateDate)))
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.Type));
        }
    }
}