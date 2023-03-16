using AuthService.Grpc;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using static AuthService.Grpc.AuthService;

namespace WebApiGateway.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly Mock<ILogger<AuthController>> _mocklogger;
        private readonly Mock<GrpcClientFactory> _mockgrpcClientFactory;
        private readonly Mock<IMapper> _mockmapper;
        private readonly IMapper _mapper;

        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            //Init moqs 
            _mocklogger = new();
            _mockgrpcClientFactory = new();
            _mockmapper = new();

            // Настройка AutoMapper
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BasicUserModel, AuthenticateRequest>()
                        .ReverseMap();

                    cfg.CreateMap<CreateUserModel, CreateUserRequest>()
                        .ReverseMap();

                    cfg.CreateMap<UserModel, User>()
                        .ReverseMap();
                });
            _mapper = new AutoMapper.Mapper(config);

            //Create Controller
            _controller = new AuthController(
                _mocklogger.Object,
                _mockgrpcClientFactory.Object,
                _mapper);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                RequestAborted = _ctoken
            };
        }

        [Fact]
        public async void CreateUserTest()
        {
            //Assert
            Mock<AuthServiceClient> authServiceClient = new Mock<AuthServiceClient>();

            GetAllRolesResponse getAllRolesResponse = new GetAllRolesResponse()
            {
                Roles =
                {
                    new Role()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "user"
                    }
                }
            };

            var asyncUnaryCall = AsyncUnaryCallBuilder(getAllRolesResponse);

            authServiceClient
                 .Setup(f => f.GetAllRolesAsync(
                     It.IsAny<GetAllRolesRequest>(),
                     null, null,
                     It.IsAny<CancellationToken>()))
                 .Returns(asyncUnaryCall);

            CreateUserResponse createUserResponse = new CreateUserResponse()
            {
                User = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user99@gmail.com",
                    IsLocked = false
                }
            };

            var asyncUnaryCall2 = AsyncUnaryCallBuilder(createUserResponse);

            authServiceClient
                .Setup(f => f.CreateUserAsync(
                    It.IsAny<CreateUserRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(asyncUnaryCall2);

            _mockgrpcClientFactory
                .Setup(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()))
                    .Returns(authServiceClient.Object);

            BasicUserModel basicUserModel = new BasicUserModel()
            {
                Email = "user99@gmail.com",
                Password = "12345678Pp!"
            };

            var responce = new UserModel()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user99@gmail.com",
                IsLocked = false
            };

            var expectedResult = new ApiResponse<UserModel>(responce);

            //Act
            var result = await _controller.CreateUser(basicUserModel);

            var okObjectResult = (OkObjectResult)result.Result;
            var apiValue = (ApiResponse<UserModel>)okObjectResult.Value;

            //Assert
            //Verify method use
            _mockgrpcClientFactory
                .Verify(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()), Times.Once());

            //Verify method use
            authServiceClient
                .Verify(f => f.GetAllRolesAsync(
                    It.IsAny<GetAllRolesRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            //Verify method use
            authServiceClient
                .Verify(f => f.CreateUserAsync(
                It.IsAny<CreateUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal(expectedResult.Data.Email, apiValue.Data.Email);
        }

        private AsyncUnaryCall<T> AsyncUnaryCallBuilder<T>(T result) where T : class
        {
            AsyncUnaryCall<T> asyncUnaryCall = new AsyncUnaryCall<T>(
                Task.FromResult(result),
                null,
                null,
                null,
                null,
                null
            );

            return asyncUnaryCall;
        }
    }
}