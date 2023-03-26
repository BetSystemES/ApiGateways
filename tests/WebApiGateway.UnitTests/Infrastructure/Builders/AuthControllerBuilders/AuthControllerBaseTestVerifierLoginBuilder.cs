using AuthService.Grpc;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.AuthControllerBuilders;

public class AuthControllerBaseTestVerifierLoginBuilder : AuthControllerBaseTestVerifierBuilder<BasicUserModel, AuthenticateResponse, Token>
{
    public override AuthControllerBaseTestVerifierBuilder<BasicUserModel, AuthenticateResponse, Token>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
        string? email = paramsStrings.ElementAtOrDefault(0);
        string? password = paramsStrings.ElementAtOrDefault(1);

        _authServiceClientRequest = Builder<BasicUserModel>
            .CreateNew()
            .With(x => x.Email = string.IsNullOrEmpty(email) ? "user99@gmail.com" : email)
            .With(x => x.Password = string.IsNullOrEmpty(password) ? "!Qwerty999^" : password)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicUserModel, AuthenticateResponse, Token>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        Token token = Builder<Token>
            .CreateNew()
            .Build();

        _authServiceClientResponse = Builder<AuthenticateResponse>
            .CreateNew()
            .With(x => x.Token = token)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicUserModel, AuthenticateResponse, Token>
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.AuthenticateAsync(
                It.IsAny<AuthenticateRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse)
            .Verifiable();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicUserModel, AuthenticateResponse, Token>
        SetExpectedResult(params string[] paramsStrings)
    {
        _expectedResult = Builder<ApiResponse<Token>>
            .CreateNew()
            .With(x => x.Data = Builder<Token>
                .CreateNew()
                .Build())
            .Build();

        return this;
    }
}