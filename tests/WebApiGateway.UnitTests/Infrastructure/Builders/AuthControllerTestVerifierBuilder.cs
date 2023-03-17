using AuthService.Grpc;
using AutoMapper;
using FizzWare.NBuilder;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Mapper.AuthService;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
using static AuthService.Grpc.AuthService;
using Grpc.Net.ClientFactory;
using WebApiGateway.UnitTests.Infrastructure.Verifiers;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerTestVerifierBuilder
{
    private AuthController? _authController;

    private Mock<GrpcClientFactory> _mockGrpcClientFactory = new();
    private Mock<AuthServiceClient> _mockAuthServiceClient = new();

    private BasicUserModel _createUserRequestModel = new();
    private CreateUserResponse _createGrpcUserResponse = new();
    private ApiResponse<UserModel> _createUserExpectedResultModel = new();
    private GetAllRolesResponse _grpcGetAllRolesResponse = new();
    private RepeatedField<Role> _grpcRoles = new();

    public AuthControllerTestVerifierBuilder Prepare()
    {
        var mockLogger = new Mock<ILogger<AuthController>>();
        _mockGrpcClientFactory = new Mock<GrpcClientFactory>();

        var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<AuthModelMap>(); }));

        _authController = new AuthController(
            mockLogger.Object,
            _mockGrpcClientFactory.Object,
            mapper);

        _authController.ControllerContext.HttpContext = new DefaultHttpContext
        {
            RequestAborted = CancellationToken.None
        };

        return this;
    }

    public AuthControllerTestVerifierBuilder AddRoles(AuthRole authRole, int size = 5)
    {
        var grpcRoles = Builder<Role>
            .CreateListOfSize(size)
            .All()
            .With(x => x.Id = Guid.NewGuid().ToString())
            .With(x => x.Name = authRole.GetDescription())
            .Build();

        _grpcRoles.Add(grpcRoles);

        return this;
    }

    public AuthControllerTestVerifierBuilder SetGrpcGetAllRolesResponse()
    {
        _grpcGetAllRolesResponse = new GetAllRolesResponse
        {
            Roles =
            {
                _grpcRoles
            }
        };

        return this;
    }

    public AuthControllerTestVerifierBuilder SetupGrpcGetAllRolesResponse()
    {
        var grpcAsyncUnaryCall = GrpcAsyncUnaryCallBuilder(_grpcGetAllRolesResponse);

        _mockAuthServiceClient
            .Setup(f => f.GetAllRolesAsync(
                It.IsAny<GetAllRolesRequest>(),
                null, null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcAsyncUnaryCall);

        return this;
    }

    public AuthControllerTestVerifierBuilder SetGrpcCreateUserResponse(Guid id, string? email = null, bool? isLocked = null)
    {
        _createGrpcUserResponse = Builder<CreateUserResponse>
            .CreateNew()
            .With(x => x.User = Builder<User>
                .CreateNew()
                .With(x => x.Id = id.ToString())
                .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
                .With(x => x.IsLocked = isLocked.HasValue && isLocked.Value)
                .Build())
            .Build();

        return this;
    }

    public AuthControllerTestVerifierBuilder SetupGrpcCreateUserResponse()
    {
        var grpcAsyncUnaryCall = GrpcAsyncUnaryCallBuilder(_createGrpcUserResponse);

        _mockAuthServiceClient
            .Setup(f => f.CreateUserAsync(
                It.IsAny<CreateUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcAsyncUnaryCall);

        return this;
    }

    public AuthControllerTestVerifierBuilder SetupGrpcClientFactory()
    {
        _mockGrpcClientFactory
            .Setup(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()))
            .Returns(_mockAuthServiceClient.Object);

        return this;
    }

    public AuthControllerTestVerifierBuilder SetCreateUserRequestModel(string? email = null, string? password = null)
    {
        _createUserRequestModel = Builder<BasicUserModel>
            .CreateNew()
            .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
            .With(x => x.Password = string.IsNullOrEmpty(password) ? "!Qwerty999^" : password)
            .Build();

        return this;
    }

    public AuthControllerTestVerifierBuilder SetCreateUserExpectedResultModel(Guid id, string? email = null, bool? isLocked = null)
    {
        _createUserExpectedResultModel = Builder<ApiResponse<UserModel>>
            .CreateNew()
            .With(x => x.Data = Builder<UserModel>
                .CreateNew()
                .With(y => y.Id = id.ToString())
                .With(y => y.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
                .With(y => y.IsLocked = isLocked.HasValue && isLocked.Value)
                .Build())
            .Build();

        return this;
    }

    public AuthControllerTestVerifier Build()
    {
        if (_authController is null)
            throw new InvalidOperationException(
                $"{nameof(AuthControllerTestVerifierBuilder)} setup is wrong. Use Prepare method before build");

        return new AuthControllerTestVerifier(_authController, _createUserRequestModel, _mockGrpcClientFactory,
            _mockAuthServiceClient, _createUserExpectedResultModel);
    }

    private AsyncUnaryCall<T> GrpcAsyncUnaryCallBuilder<T>(T result) where T : class
    {
        var asyncUnaryCall = new AsyncUnaryCall<T>(
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