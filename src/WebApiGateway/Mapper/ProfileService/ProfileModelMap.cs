using AutoMapper;
using ProfileService.GRPC;
using WebApiGateway.Models.ProfileService;

namespace WebApiGateway.Mapper.ProfileService
{
    public class ProfileModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileModelMap"/> class.
        /// </summary>
        public ProfileModelMap()
        {
            CreateMap<ProfileModel, PersonalProfile>()
                .ReverseMap();
        }
    }
}
