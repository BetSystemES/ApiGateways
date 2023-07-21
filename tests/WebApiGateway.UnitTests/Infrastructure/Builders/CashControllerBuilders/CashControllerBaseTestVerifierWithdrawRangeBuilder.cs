using CashService.GRPC;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierWithdrawRangeBuilder : CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, WithdrawRangeResponse, List<TransactionModelCreateModel>>
    {
        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, WithdrawRangeResponse, List<TransactionModelCreateModel>>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _cashServiceClientRequest = Builder<TransactionModelCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build()
                .ToList();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, WithdrawRangeResponse, List<TransactionModelCreateModel>>
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

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, WithdrawRangeResponse, List<TransactionModelCreateModel>>
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

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, WithdrawRangeResponse, List<TransactionModelCreateModel>>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);

            var transactionApis = Builder<TransactionCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModelApi = Builder<TransactionModelCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build();

            _expectedResult = Builder<ApiResponse<List<TransactionModelCreateModel>>>
                .CreateNew()
                .With(x => x.Data = transactionModelApi.ToList())
                .Build();

            return this;
        }
    }
}
