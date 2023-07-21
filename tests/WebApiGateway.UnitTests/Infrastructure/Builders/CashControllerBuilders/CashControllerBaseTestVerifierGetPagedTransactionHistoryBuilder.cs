using CashService.GRPC;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierGetPagedTransactionHistoryBuilder : CashControllerBaseTestVerifierBuilder<CashServiceRequestModel, GetPagedTransactionsHistoryResponse, BasePagedResponseModel<TransactionModelCreateModel>>
    {
        public override CashControllerBaseTestVerifierBuilder<CashServiceRequestModel, GetPagedTransactionsHistoryResponse, BasePagedResponseModel<TransactionModelCreateModel>>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _cashServiceClientRequest = Builder<CashServiceRequestModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<CashServiceRequestModel, GetPagedTransactionsHistoryResponse, BasePagedResponseModel<TransactionModelCreateModel>>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var transactions = Builder<Transaction>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModels = Builder<TransactionModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            _cashServiceClientResponse = Builder<GetPagedTransactionsHistoryResponse>
                .CreateNew()
                .Build();

            _cashServiceClientResponse.Transactions.AddRange(transactionModels);

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<CashServiceRequestModel, GetPagedTransactionsHistoryResponse, BasePagedResponseModel<TransactionModelCreateModel>>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.GetPagedTransactionHistoryAsync(
                    It.IsAny<GetTransactionHistoryWithFilterRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<CashServiceRequestModel, GetPagedTransactionsHistoryResponse, BasePagedResponseModel<TransactionModelCreateModel>>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var transactionApis = Builder<TransactionCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModelApi = Builder<TransactionModelCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build();

            _expectedResult = Builder<ApiResponse<BasePagedResponseModel<TransactionModelCreateModel>>>
                .CreateNew()
                .With(x => x.Data = Builder<BasePagedResponseModel<TransactionModelCreateModel>>
                    .CreateNew()
                    .With(y => y.Data = transactionModelApi.ToList())
                    .Build())
                .Build();

            return this;
        }
    }
}
