using AuthService.Grpc;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerBaseTestVerifierRefreshTokenBuilder : AuthControllerBaseTestVerifierBuilder<BasicTokenModel, RefreshResponse, Token>
{
    public override AuthControllerBaseTestVerifierBuilder<BasicTokenModel, RefreshResponse, Token>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
        string? token = paramsStrings.ElementAtOrDefault(0);

        _authServiceClientRequest = Builder<BasicTokenModel>
            .CreateNew()
            .With(x => x.AuthToken = string.IsNullOrEmpty(token) ? "kjnajfnshnfsj" : token)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicTokenModel, RefreshResponse, Token>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        Token token = Builder<Token>
            .CreateNew()
            .Build();

        _authServiceClientResponse = Builder<RefreshResponse>
            .CreateNew()
            .With(x => x.Token = token)
            .Build();

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicTokenModel, RefreshResponse, Token>
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.RefreshAsync(
                It.IsAny<RefreshRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse);

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<BasicTokenModel, RefreshResponse, Token>
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