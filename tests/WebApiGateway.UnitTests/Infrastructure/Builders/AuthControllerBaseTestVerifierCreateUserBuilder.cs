using AuthService.Grpc;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.UnitTests.Infrastructure.Builders.Extensions;
using static WebApiGateway.UnitTests.Infrastructure.Builders.Helpers.GuidHelper;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerBaseTestVerifierCreateUserBuilder : AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
{
    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
        string? email = paramsStrings.ElementAtOrDefault(0);
        string? password = paramsStrings.ElementAtOrDefault(1);
        int rolesListSize = int.TryParse(paramsStrings.ElementAtOrDefault(2), out int result) ? result : 1;

        var roleIds = GenerateGuidList(rolesListSize);

        _authServiceClientRequest = Builder<CreateUserModel>
            .CreateNew()
            .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
            .With(x => x.Password = string.IsNullOrEmpty(password) ? "!Qwerty999^" : password)
            .With(x => x.RoleIds = roleIds.Select(guid=> guid.ToString()))
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        string? email = paramsStrings.ElementAtOrDefault(0);
        bool? isLocked = paramsStrings.ElementAtOrDefault(1).Convert<bool>();

        _authServiceClientResponse = Builder<CreateUserResponse>
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

    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel> 
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.CreateUserAsync(
                It.IsAny<CreateUserRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse);

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
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