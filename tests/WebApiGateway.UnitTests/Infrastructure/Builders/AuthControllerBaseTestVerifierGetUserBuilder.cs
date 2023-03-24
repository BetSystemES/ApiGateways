using AuthService.Grpc;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.UnitTests.Infrastructure.Builders.Extensions;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerBaseTestVerifierGetUserBuilder : AuthControllerBaseTestVerifierBuilder<BaseUserRequestModel, GetUserResponse, UserModel>
{
    public override AuthControllerBaseTestVerifierBuilder<BaseUserRequestModel, GetUserResponse, UserModel>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
        string? userId = paramsStrings.ElementAtOrDefault(0);

        _authServiceClientRequest = Builder<BaseUserRequestModel>
            .CreateNew()
            .With(x => x.UserId = string.IsNullOrEmpty(userId) ? _userId : Guid.Parse(userId))
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BaseUserRequestModel, GetUserResponse, UserModel>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        string? email = paramsStrings.ElementAtOrDefault(0);
        bool? isLocked = paramsStrings.ElementAtOrDefault(1).Convert<bool>();

        _authServiceClientResponse = Builder<GetUserResponse>
            .CreateNew()
            .With(x => x.User = Builder<User>
                .CreateNew()
                .With(x => x.Id = _userId.ToString())
                .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
                .With(x => x.IsLocked = isLocked.HasValue && isLocked.Value)
                .Build())
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BaseUserRequestModel, GetUserResponse, UserModel> 
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.GetUserAsync(
                It.IsAny<GetUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse)
            .Verifiable();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BaseUserRequestModel, GetUserResponse, UserModel>
        SetExpectedResult(params string[] paramsStrings)
    {
        string? email = paramsStrings.ElementAtOrDefault(0);
        bool? isLocked = paramsStrings.ElementAtOrDefault(1).Convert<bool>();

        _expectedResult = Builder<ApiResponse<UserModel>>
            .CreateNew()
            .With(x => x.Data = Builder<UserModel>
                .CreateNew()
                .With(y => y.Id = _userId.ToString())
                .With(y => y.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
                .With(y => y.IsLocked = isLocked.HasValue && isLocked.Value)
            .Build())
        .Build();
        return this;
    }
}