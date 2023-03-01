using AutoMapper;
using ProfileService.GRPC;
using WebApiGateway.Models.ProfileService;

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

            // TODO: this one can be removed. It is unnecessary
            CreateMap<IEnumerable<DiscountModel>, IEnumerable<Discount>>();
        }
    }
}
