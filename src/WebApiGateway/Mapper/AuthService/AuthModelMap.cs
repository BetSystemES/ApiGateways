using AuthService.Grpc;
using AutoMapper;
using WebApiGateway.Models.AuthService;

namespace WebApiGateway.Mapper.AuthService
{
    public class AuthProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthProfile"/> class.
        /// </summary>
        public AuthProfile()
        {
            CreateMap<BasicUserModel, AuthenticateRequest>()
                .ReverseMap();

            CreateMap<CreateUserModel, CreateUserRequest>()
                .ReverseMap();

            CreateMap<UserModel, User>()
                .ReverseMap();

            CreateMap<RoleModel, Role>()
                .ReverseMap();
        }
    }
}