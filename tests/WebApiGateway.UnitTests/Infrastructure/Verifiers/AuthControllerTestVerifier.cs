using AuthService.Grpc;
using Grpc.Net.ClientFactory;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;
using static AuthService.Grpc.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Verifiers;

public class AuthControllerTestVerifier<TRequest, TExpectedResult>
    where TRequest : class
    where TExpectedResult : class
{
    public AuthControllerTestVerifier(
        AuthController authController,
        TRequest requestModel,
        Mock<GrpcClientFactory> mockGrpcClientFactory,
        Mock<AuthServiceClient> authServiceClient,
        ApiResponse<TExpectedResult> expectedResult)
    {
        AuthController = authController;
        RequestModel = requestModel;
        MockGrpcClientFactory = mockGrpcClientFactory;
        AuthServiceClient = authServiceClient;
        ExpectedResult = expectedResult;
    }

    public AuthController AuthController { get; }
    public Mock<GrpcClientFactory> MockGrpcClientFactory { get; }
    public Mock<AuthServiceClient> AuthServiceClient { get; }
    public TRequest RequestModel { get; }
    public ApiResponse<TExpectedResult> ExpectedResult { get; }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyGrpcClientFactoryCreateClient()
    {
        MockGrpcClientFactory
            .Verify(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyAuthServiceClientCreateUserAsync()
    {
        AuthServiceClient
            .Verify(f => f.CreateUserAsync(
                It.IsAny<CreateUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyAuthServiceClientAuthenticateAsync()
    {
        AuthServiceClient
            .Verify(f => f.AuthenticateAsync(
                It.IsAny<AuthenticateRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyAuthServiceClientRefreshAsync()
    {
        AuthServiceClient
            .Verify(f => f.RefreshAsync(
                It.IsAny<RefreshRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyAuthServiceClientGetUserAsync()
    {
        AuthServiceClient
            .Verify(f => f.GetUserAsync(
                It.IsAny<GetUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier<TRequest, TExpectedResult> VerifyAuthServiceClientGetAllRolesAsync()
    {
        AuthServiceClient
            .Verify(f => f.GetAllRolesAsync(
                It.IsAny<GetAllRolesRequest>(),
                null, null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }
}