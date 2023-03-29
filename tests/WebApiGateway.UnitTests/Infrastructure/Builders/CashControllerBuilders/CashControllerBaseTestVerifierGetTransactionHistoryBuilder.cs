using CashService.GRPC;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierGetTransactionHistoryBuilder : CashControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetTransactionsHistoryResponse, TransactionModelApi>
    {
        public override CashControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetTransactionsHistoryResponse, TransactionModelApi>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _cashServiceClientRequest = Builder<BaseProfileRequstModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetTransactionsHistoryResponse, TransactionModelApi>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var transactions = Builder<Transaction>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var balance = Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();
            balance.Transactions.AddRange(transactions);

            _cashServiceClientResponse = Builder<GetTransactionsHistoryResponse>
                .CreateNew()
                .With(x => x.Balance = balance)
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetTransactionsHistoryResponse, TransactionModelApi>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.GetTransactionsHistoryAsync(
                    It.IsAny<GetTransactionsHistoryRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetTransactionsHistoryResponse, TransactionModelApi>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);

            var transactionApis = Builder<TransactionApi>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModelApi = Builder<TransactionModelApi>
                .CreateNew()
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build();

            _expectedResult = Builder<ApiResponse<TransactionModelApi>>
                .CreateNew()
                .With(x => x.Data = transactionModelApi)
                .Build();

            return this;
        }
    }
}
