using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.UnitTests.Infrastructure;
using WebApiGateway.UnitTests.Infrastructure.Builders;
using Xunit.Abstractions;

namespace WebApiGateway.UnitTests.Controllers;

public class AuthControllerTests : BaseTest
{
    public AuthControllerTests(ITestOutputHelper testOutputHelper, string category = Constants.UnitTest) : base(
        testOutputHelper, category)
    {
    }

    [Fact]
    [Trait(Constants.Category, Constants.UnitTest)]
    public async void CreateUserTest()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var verifier = new AuthControllerBaseTestVerifierCreateUserBuilder()
            .Prepare()
            .AddAuthServiceClientRoles(AuthRole.User, 1)
            .SetUserId(userId)
            .SetAuthServiceClientRequest()
            .SetAuthServiceClientResponse()
            .SetupAuthServiceClientResponse()
            .SetupAuthServiceClientGrpcFactory()
            .SetExpectedResult()
            .Build();

        // Act
        var result = await verifier.AuthController.CreateUser(verifier.CreateUserRequestModel);
        result.Result.Should().NotBeNull();

        Logger.LogInformation($"AuthController.CreateUser result: {Serialize(result)}");

        var actionResult = (ObjectResult) result.Result!;
        actionResult.Value.Should().NotBeNull();

        Logger.LogInformation($"actionResult result: {Serialize(actionResult)}");

        var apiResponse = (ApiResponse<UserModel>) actionResult.Value!;
        apiResponse.Should().NotBeNull();

        Logger.LogInformation($"apiResponse result: {Serialize(apiResponse)}");
        Logger.LogInformation(
            $"verifier.ExpectedResult result: {Serialize(verifier.ExpectedResult)}");

        // Assert
        verifier
            .VerifyGrpcClientFactoryCreateClient()
            .VerifyAuthServiceClientCreateUserAsync();

        verifier.ExpectedResult.Data.Should().NotBeNull();
        apiResponse.Data.Should().NotBeNull();

        verifier.ExpectedResult.Data!.Id.Should().Be(apiResponse.Data!.Id);
        verifier.ExpectedResult.Data.Email.Should().Be(apiResponse.Data.Email);
        verifier.ExpectedResult.Data.IsLocked.Should().Be(apiResponse.Data.IsLocked);
    }
}