using CashService.GRPC;
using FizzWare.NBuilder;
using Moq;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierWithdrawBuilder : CashControllerBaseTestVerifierBuilder<TransactionModelApi, WithdrawResponse, TransactionModelApi>
    {
        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, WithdrawResponse, TransactionModelApi>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _cashServiceClientRequest = Builder<TransactionModelApi>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, WithdrawResponse, TransactionModelApi>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var transactionApis = Builder<Transaction>
                .CreateListOfSize(3)
                .All()
                .With(x => x.TransactionId = id ?? Guid.NewGuid().ToString())
                .Build();

            var transactionModel = Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            transactionModel.Transactions.AddRange(transactionApis);

            _cashServiceClientResponse = Builder<WithdrawResponse>
                .CreateNew()
                .With(x => x.Withdrawresponse = transactionModel)
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, WithdrawResponse, TransactionModelApi>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.WithdrawAsync(
                    It.IsAny<WithdrawRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, WithdrawResponse, TransactionModelApi>
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
