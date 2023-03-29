using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Xunit.Abstractions;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.CashService;
using WebApiGateway.UnitTests.Infrastructure;
using WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders;

namespace WebApiGateway.UnitTests.Controllers
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class CashControllerTests : BaseTest
    {
        public CashControllerTests(ITestOutputHelper testOutputHelper, string category = Constants.UnitTest) : base(testOutputHelper, category)
        {
        }

        [Fact]
        public async Task GetTransactionHistoryTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new CashControllerBaseTestVerifierGetTransactionHistoryBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.CashController.GetTransactionsHistory(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<TransactionModelApi>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();
            
            apiResponse.Data.TransactionApis.Select(x => x.TransactionId).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.TransactionApis.Select(x => x.TransactionId));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientGetTransactionHistoryAsync();
        }

        [Fact]
        public async Task GetPagedTransactionHistoryTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            var verifier = new CashControllerBaseTestVerifierGetPagedTransactionHistoryBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id, userId)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id, userId)
                .Build();

            // Act
            var result = await verifier.CashController.GetPagedTransactionHistory(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<BasePagedResponseModel<TransactionModelApi>>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.Data.Select(x => x.ProfileId).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.Data.Select(x => x.ProfileId));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientGetPagedTransactionHistoryAsync();
        }

        [Fact]
        public async Task GetBalanceTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new CashControllerBaseTestVerifierGetBalanceBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.CashController.GetBalance(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<TransactionModelApi>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.TransactionApis.Select(x => x.TransactionId).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.TransactionApis.Select(x => x.TransactionId));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientGetBalanceAsync();
        }

        [Fact]
        public async Task DepositTest()
        {
            // Arrange
            var verifier = new CashControllerBaseTestVerifierDepositBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse()
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .Build();

            // Act
            await verifier.CashController.Deposit(verifier.RequestModel);

            // Assert
            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientDepositAsync();
        }

        [Fact]
        public async Task WithdrawTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new CashControllerBaseTestVerifierWithdrawBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.CashController.Withdraw(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<TransactionModelApi>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.TransactionApis.Select(x => x.TransactionId).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.TransactionApis.Select(x => x.TransactionId));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientWithdrawAsync();
        }

        [Fact]
        public async Task DepositRangeTest()
        {
            // Arrange
            var verifier = new CashControllerBaseTestVerifierDepositRangeBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse()
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .Build();

            // Act
            await verifier.CashController.DepositRange(verifier.RequestModel);

            // Assert
            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientDepositRangeAsync();
        }

        [Fact]
        public async Task WithdrawRangeTest()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            var verifier = new CashControllerBaseTestVerifierWithdrawRangeBuilder()
                .Prepare()
                .SetProfileServiceClientRequest()
                .SetProfileServiceClientResponse(id)
                .SetupProfileServiceClientResponse()
                .SetupProfileServiceClientGrpcFactory()
                .SetExpectedResult(id)
                .Build();

            // Act
            var result = await verifier.CashController.WithdrawRange(verifier.RequestModel);
            result.Result.Should().NotBeNull();

            var actionResult = (ObjectResult)result.Result!;
            actionResult.Should().NotBeNull();

            var apiResponse = (ApiResponse<IEnumerable<TransactionModelApi>>)actionResult.Value!;
            apiResponse.Should().NotBeNull();

            // Assert
            apiResponse.Data.Should().NotBeNull();
            verifier.ExpectedResult.Data.Should().NotBeNull();

            apiResponse.Data.Select(x => x.TransactionApis.Select(x => x.TransactionId)).Should()
                .BeEquivalentTo(verifier.ExpectedResult.Data.Select(x => x.TransactionApis.Select(x => x.TransactionId)));

            verifier
                .VerifyGrpcClientFactoryCreateClient()
                .VerifyCashServiceClientWithdrawRangeAsync();
        }
    }
}
