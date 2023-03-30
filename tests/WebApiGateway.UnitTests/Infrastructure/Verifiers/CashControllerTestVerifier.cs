using CashService.GRPC;
using Grpc.Net.ClientFactory;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;
using static CashService.GRPC.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Verifiers
{
    public class CashControllerTestVerifier<TRequest, TExpectedResult>
        where TRequest : class
        where TExpectedResult : class
    {
        public CashControllerTestVerifier(
            CashController cashController,
            TRequest cashServiceClientRequest,
            Mock<GrpcClientFactory> mockGrpcClientFactory,
            Mock<CashServiceClient> mockCashServiceClient,
            ApiResponse<TExpectedResult> expectedResult)
        {
            CashController = cashController;
            RequestModel = cashServiceClientRequest;
            MockGrpcClientFactory = mockGrpcClientFactory;
            CashServiceClient = mockCashServiceClient;
            ExpectedResult = expectedResult;
        }

        public CashController CashController { get; }
        public Mock<GrpcClientFactory> MockGrpcClientFactory { get; }
        public Mock<CashServiceClient> CashServiceClient { get; }
        public TRequest RequestModel { get; }
        public ApiResponse<TExpectedResult> ExpectedResult { get; }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyGrpcClientFactoryCreateClient()
        {
            MockGrpcClientFactory
                .Verify(f => f.CreateClient<CashServiceClient>(It.IsAny<string>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientGetTransactionHistoryAsync()
        {
            CashServiceClient
                .Verify(f => f.GetTransactionsHistoryAsync(
                    It.IsAny<GetTransactionsHistoryRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientGetPagedTransactionHistoryAsync()
        {
            CashServiceClient
                .Verify(f => f.GetPagedTransactionHistoryAsync(
                    It.IsAny<GetTransactionHistoryWithFilterRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientGetBalanceAsync()
        {
            CashServiceClient
                .Verify(f => f.GetBalanceAsync(
                    It.IsAny<GetBalanceRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientDepositAsync()
        {
            CashServiceClient
                .Verify(f => f.DepositAsync(
                    It.IsAny<DepositRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientWithdrawAsync()
        {
            CashServiceClient
                .Verify(f => f.WithdrawAsync(
                    It.IsAny<WithdrawRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientDepositRangeAsync()
        {
            CashServiceClient
                .Verify(f => f.DepositRangeAsync(
                    It.IsAny<DepositRangeRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CashControllerTestVerifier<TRequest, TExpectedResult> VerifyCashServiceClientWithdrawRangeAsync()
        {
            CashServiceClient
                .Verify(f => f.WithdrawRangeAsync(
                    It.IsAny<WithdrawRangeRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }
    }
}
