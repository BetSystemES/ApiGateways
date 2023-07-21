using CashService.GRPC;
using FizzWare.NBuilder;
using Google.Protobuf.WellKnownTypes;
using Moq;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierDepositRangeBuilder : CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, DepositRangeResponse, Empty>
    {
        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, DepositRangeResponse, Empty>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            var transactionApis = Builder<TransactionCreateModel>
                .CreateListOfSize(3)
                .Build();

            _cashServiceClientRequest = Builder<TransactionModelCreateModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build()
                .ToList();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, DepositRangeResponse, Empty>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            _cashServiceClientResponse = Builder<DepositRangeResponse>
                .CreateNew()
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, DepositRangeResponse, Empty>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.DepositRangeAsync(
                    It.IsAny<DepositRangeRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<List<TransactionModelCreateModel>, DepositRangeResponse, Empty>
            SetExpectedResult(params string[] paramsStrings)
        {
            return this;
        }
    }
}
