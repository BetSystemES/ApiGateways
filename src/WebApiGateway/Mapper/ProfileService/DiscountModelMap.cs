using AutoMapper;
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

            CreateMap<DiscountModel, Discount>()
                .ReverseMap();
        }
    }
}