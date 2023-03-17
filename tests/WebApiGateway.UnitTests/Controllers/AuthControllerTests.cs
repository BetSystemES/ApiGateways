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
        var userId = Guid.NewGuid();

        // TODO: refactor unit test because CreateUser will not invoke GetAllRoles method.
        var verifier = new AuthControllerTestVerifierBuilder()
            .Prepare()
            .AddAuthServiceClientRoles(AuthRole.User, 1)
            .SetAuthServiceClientGetAllRolesResponse()
            .SetupAuthServiceClientGetAllRolesResponse()
            .SetAuthServiceClientCreateUserResponse(userId)
            .SetupAuthServiceClientCreateUserResponse()
            .SetupAuthServiceClientGrpcFactory()
            .SetCreateUserRequestModel()
            .SetCreateUserExpectedResultModel(userId)
            .Build();

        var result = await verifier.AuthController.CreateUser(verifier.CreateUserRequestModel);
        result.Result.Should().NotBeNull();

        Logger.LogInformation($"AuthController.CreateUser result: {Serialize(result)}");

        var actionResult = (OkObjectResult) result.Result!;
        actionResult.Value.Should().NotBeNull();

        Logger.LogInformation($"actionResult result: {Serialize(actionResult)}");

        var apiResponse = (ApiResponse<UserModel>) actionResult.Value!;
        apiResponse.Should().NotBeNull();

        Logger.LogInformation($"apiResponse result: {Serialize(apiResponse)}");
        Logger.LogInformation(
            $"verifier.CreateUserExpectedResultModel result: {Serialize(verifier.CreateUserExpectedResultModel)}");

        verifier
            .VerifyGrpcClientFactoryCreateClient()
            .VerifyAuthServiceClientGetAllRolesAsync()
            .VerifyAuthServiceClientCreateUserAsync();

        verifier.CreateUserExpectedResultModel.Data.Should().NotBeNull();
        apiResponse.Data.Should().NotBeNull();

        verifier.CreateUserExpectedResultModel.Data!.Id.Should().Be(apiResponse.Data!.Id);
        verifier.CreateUserExpectedResultModel.Data.Email.Should().Be(apiResponse.Data.Email);
        verifier.CreateUserExpectedResultModel.Data.IsLocked.Should().Be(apiResponse.Data.IsLocked);
    }
}