using AuthService.Grpc;
using FizzWare.NBuilder;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class
    AuthControllerBaseTestVerifierCreateUserBuilder : AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
{
    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
        SetAuthServiceClientRequest(
            string? email = null,
            string? password = null,
            int rolesListSize = 1)
    {
        var roleIds = new List<string>();
        while (rolesListSize > 0)
        {
            roleIds.Add(Guid.NewGuid().ToString());
            rolesListSize--;
        }

        _authServiceClientRequest = Builder<CreateUserModel>
            .CreateNew()
            .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
            .With(x => x.Password = string.IsNullOrEmpty(password) ? "!Qwerty999^" : password)
            .With(x => x.RoleIds = roleIds)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<CreateUserModel, CreateUserResponse, UserModel>
        SetAuthServiceClientResponse(string? email = null, bool? isLocked = null)
    {
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
        SetExpectedResult(
            string? email = null, bool? isLocked = null)
    {
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