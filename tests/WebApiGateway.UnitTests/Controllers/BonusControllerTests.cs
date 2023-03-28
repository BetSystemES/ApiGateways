using FluentAssertions;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.ProfileService;
using WebApiGateway.UnitTests.Infrastructure;
using WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders;

namespace WebApiGateway.UnitTests.Controllers
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class BonusControllerTests : BaseTest
    {
        public BonusControllerTests(ITestOutputHelper testOutputHelper, string category = Constants.UnitTest)
            : base(testOutputHelper, category)
        {
        }

        [Fact]
        public async Task GetDiscountsTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new BonusControllerBaseTestVerifierGetDiscountsBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.BonusController.GetDiscounts(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<List<DiscountModel>>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.Select(x => x.Id).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.Select(x => x.Id));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyProfileServiceClientGetDiscountsAsync();
        }

        [Fact]
        public async Task GetPagedDiscountsTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new BonusControllerBaseTestVerifierGetPagedDiscountsBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.BonusController.GetPagedDiscounts(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<BasePagedResponseModel<DiscountModel>>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.Data.Select(x => x.Id).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.Data.Select(x => x.Id));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyProfileServiceClientGetPagedDiscountsAsync();
        }

        [Fact]
        public async Task PostTest()
        {
            // Arrange
            var verifier = new BonusControllerBaseTestVerifierPostBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse()
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult()
                .Build();

            // Act
            await verifier.BonusController.Post(verifier.RequestModel);

            // Assert
            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyProfileServiceClientAddDiscountAsync();
        }

        [Fact]
        public async Task PutTest()
        {
            // Arrange
            var verifier = new BonusControllerBaseTestVerifierPutBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse()
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult()
                .Build();

            // Act
            await verifier.BonusController.Put(verifier.RequestModel);

            // Assert
            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyProfileServiceClientUpdateDiscountAsync();
        }
    }
}
