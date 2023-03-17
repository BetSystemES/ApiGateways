using AuthService.Grpc;
using Grpc.Net.ClientFactory;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using static AuthService.Grpc.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Verifiers;

public class AuthControllerTestVerifier
{
    public AuthController AuthController { get; }
    public BasicUserModel BasicUserModel { get; }
    public Mock<GrpcClientFactory> MockGrpcClientFactory { get; }
    public Mock<AuthServiceClient> AuthServiceClient { get; }
    public ApiResponse<UserModel> ExpectedResultUserModel { get; }

    public AuthControllerTestVerifier(
        AuthController authController,
        BasicUserModel basicUserModel,
        Mock<GrpcClientFactory> mockGrpcClientFactory,
        Mock<AuthServiceClient> authServiceClient,
        ApiResponse<UserModel> expectedResultUserModel)
    {
        AuthController = authController;
        BasicUserModel = basicUserModel;
        MockGrpcClientFactory = mockGrpcClientFactory;
        AuthServiceClient = authServiceClient;
        ExpectedResultUserModel = expectedResultUserModel;
    }

    public AuthControllerTestVerifier VerifyGrpcClientFactoryCreateClient()
    {
        MockGrpcClientFactory.Verify(f => f.CreateClient<AuthServiceClient>(It.IsAny<string>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier VerifyAuthServiceClientGetAllRolesAsync()
    {
        AuthServiceClient
            .Verify(f => f.GetAllRolesAsync(
                It.IsAny<GetAllRolesRequest>(),
                null, null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }

    public AuthControllerTestVerifier VerifyAuthServiceClientCreateUserAsync()
    {
        AuthServiceClient
            .Verify(f => f.CreateUserAsync(
                It.IsAny<CreateUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()), Times.Once());

        return this;
    }
}