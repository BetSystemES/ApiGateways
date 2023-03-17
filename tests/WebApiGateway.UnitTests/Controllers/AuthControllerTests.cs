﻿using FluentAssertions;
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

        var verifier = new AuthControllerTestBuilder()
            .Prepare()
            .AddRoles(AuthRole.User, 1)
            .SetGetAllRolesResponse()
            .SetupGetAllRolesResponse()
            .SetCreateUserResponse(userId)
            .SetupCreateUserResponse()
            .SetupGrpcClientFactory()
            .SetBasicUserModel()
            .SetExpectedResultUserModel(userId)
            .Build();

        var result = await verifier.AuthController.CreateUser(verifier.BasicUserModel);
        result.Result.Should().NotBeNull();

        Logger.LogInformation($"AuthController.CreateUser result: {Serialize(result)}");

        var actionResult = (OkObjectResult) result.Result!;
        actionResult.Value.Should().NotBeNull();

        Logger.LogInformation($"actionResult result: {Serialize(actionResult)}");

        var apiResponse = (ApiResponse<UserModel>) actionResult.Value!;
        apiResponse.Should().NotBeNull();

        Logger.LogInformation($"apiResponse result: {Serialize(apiResponse)}");
        Logger.LogInformation(
            $"verifier.ExpectedResultUserModel result: {Serialize(verifier.ExpectedResultUserModel)}");

        verifier
            .VerifyGrpcClientFactoryCreateClient()
            .VerifyAuthServiceClientGetAllRolesAsync()
            .VerifyAuthServiceClientCreateUserAsync();

        verifier.ExpectedResultUserModel.Data.Should().NotBeNull();
        apiResponse.Data.Should().NotBeNull();

        verifier.ExpectedResultUserModel.Data!.Id.Should().Be(apiResponse.Data!.Id);
        verifier.ExpectedResultUserModel.Data.Email.Should().Be(apiResponse.Data.Email);
        verifier.ExpectedResultUserModel.Data.IsLocked.Should().Be(apiResponse.Data.IsLocked);
    }
}