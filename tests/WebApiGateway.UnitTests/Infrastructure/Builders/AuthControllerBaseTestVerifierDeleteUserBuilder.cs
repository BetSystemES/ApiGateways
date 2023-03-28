using AuthService.Grpc;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.UnitTests.Infrastructure.Builders.AuthControllerBuilders;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerBaseTestVerifierDeleteUserBuilder : AuthControllerBaseTestVerifierBuilder<DeleteUserModel, DeleteUserResponse, String>
{
    public override AuthControllerBaseTestVerifierBuilder<DeleteUserModel, DeleteUserResponse, String>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
        string ? email = paramsStrings.ElementAtOrDefault(0);

        _authServiceClientRequest = Builder<DeleteUserModel>
            .CreateNew()
            .With(x => x.UserId = _userId)
            .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<DeleteUserModel, DeleteUserResponse, String>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        _authServiceClientResponse = Builder<DeleteUserResponse>
            .CreateNew()
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<DeleteUserModel, DeleteUserResponse, String> 
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.DeleteUserAsync(
                It.IsAny<DeleteUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse)
            .Verifiable();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<DeleteUserModel, DeleteUserResponse, String>
        SetExpectedResult(params string[] paramsStrings)
    {
        _expectedResult = Builder<ApiResponse<string>>
            .CreateNew()
            .Build();
        return this;
    }
}