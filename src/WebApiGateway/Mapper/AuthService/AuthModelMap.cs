using AuthService.Grpc;
using AutoMapper;
using WebApiGateway.Models.AuthService;

namespace WebApiGateway.Mapper.AuthService
{
    public class AuthModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthModelMap"/> class.
        /// </summary>
        public AuthModelMap()
        {
            CreateMap<AuthenticateModel, AuthenticateRequest>()
                .ReverseMap();
        }
    }
}