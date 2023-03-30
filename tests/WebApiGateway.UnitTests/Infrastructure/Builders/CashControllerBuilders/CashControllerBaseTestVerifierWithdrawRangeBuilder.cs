using CashService.GRPC;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierWithdrawRangeBuilder : CashControllerBaseTestVerifierBuilder<List<TransactionModelApi>, WithdrawRangeResponse, List<TransactionModelApi>>
    {
        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelApi>, WithdrawRangeResponse, List<TransactionModelApi>>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _cashServiceClientRequest = Builder<TransactionModelApi>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build()
                .ToList();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelApi>, WithdrawRangeResponse, List<TransactionModelApi>>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var transactionApis = Builder<Transaction>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModels = Builder<TransactionModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .And(x => x.Transactions.AddRange(transactionApis))
                .Build();

            _cashServiceClientResponse = Builder<WithdrawRangeResponse>
                .CreateNew()
                .Build();

            _cashServiceClientResponse.WithdrawRangeResponses.AddRange(transactionModels);

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelApi>, WithdrawRangeResponse, List<TransactionModelApi>>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.WithdrawRangeAsync(
                    It.IsAny<WithdrawRangeRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelApi>, WithdrawRangeResponse, List<TransactionModelApi>>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);

            var transactionApis = Builder<TransactionApi>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModelApi = Builder<TransactionModelApi>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build();

            _expectedResult = Builder<ApiResponse<List<TransactionModelApi>>>
                .CreateNew()
                .With(x => x.Data = transactionModelApi.ToList())
                .Build();

            return this;
        }
    }
}
