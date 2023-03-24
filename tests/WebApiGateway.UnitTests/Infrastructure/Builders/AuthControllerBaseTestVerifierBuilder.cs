using AuthService.Grpc;
using AutoMapper;
using FizzWare.NBuilder;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Mapper.AuthService;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
using WebApiGateway.UnitTests.Infrastructure.Verifiers;
using static AuthService.Grpc.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public abstract class AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
    where TRequest : class, new()
    where TResponse : class, new()
    where TExpectedResult : class
{
    protected AuthController? _authController;
    protected GetAllRolesResponse _authServiceClientGetAllRolesResponse = new();

    protected TRequest _authServiceClientRequest = new();
    protected TResponse _authServiceClientResponse = new();

    protected RepeatedField<Role> _authServiceClientRoles = new();
    protected ApiResponse<TExpectedResult> _expectedResult = new();
    protected Mock<AuthServiceClient> _mockAuthServiceClient = new();

    protected Mock<GrpcClientFactory> _mockGrpcClientFactory = new();
    protected List<string> _roleIds = new();

    protected Guid _userId;

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult> Prepare()
    {
        var mockLogger = new Mock<ILogger<AuthController>>();
        _mockGrpcClientFactory = new Mock<GrpcClientFactory>();

        var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AuthProfile>()));

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

    public abstract AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetAuthServiceClientRequest(params string[] paramsStrings);

    public abstract AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetAuthServiceClientResponse(params string[] paramsStrings);

    public abstract AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetupAuthServiceClientResponse();

    public abstract AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetExpectedResult(params string[] paramsStrings);

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult> SetUserId(Guid id)
    {
        _userId = id;
        return this;
    }

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult> AddAuthServiceClientRoles(
        AuthRole authRole, int size = 5)
    {
        var authServiceClientRoles = Builder<Role>
            .CreateListOfSize(size)
            .All()
            .With(x => x.Id = _userId.ToString())
            .With(x => x.Name = authRole.GetDescription())
            .Build();

        _authServiceClientRoles.Add(authServiceClientRoles);

        return this;
    }

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetAuthServiceClientGetAllRolesResponse()
    {
        _authServiceClientGetAllRolesResponse = new GetAllRolesResponse
        {
            Roles =
            {
                _authServiceClientRoles
            }
        };

        return this;
    }

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetupAuthServiceClientGrpcFactory()
    {
        _mockGrpcClientFactory
            .Setup(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()))
            .Returns(_mockAuthServiceClient.Object)
            .Verifiable();

        return this;
    }

    public AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        SetupAuthServiceClientGetAllRolesResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientGetAllRolesResponse);

        _mockAuthServiceClient
            .Setup(f => f.GetAllRolesAsync(
                It.IsAny<GetAllRolesRequest>(),
                null, null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse)
            .Verifiable();

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> Build()
    {
        if (_authController is null)
            throw new InvalidOperationException(
                $"{nameof(AuthControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>)} setup is wrong. Use Prepare method before build");

        return new AuthControllerTestVerifier<TRequest, TExpectedResult>(_authController, _authServiceClientRequest,
            _mockGrpcClientFactory,
            _mockAuthServiceClient, _expectedResult);
    }

    protected AsyncUnaryCall<T> GrpcAsyncUnaryCallBuilder<T>(T result) where T : class
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