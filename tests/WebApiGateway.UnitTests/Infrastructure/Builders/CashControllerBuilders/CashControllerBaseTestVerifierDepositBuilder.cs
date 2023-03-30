using CashService.GRPC;
using FizzWare.NBuilder;
using Google.Protobuf.WellKnownTypes;
using Moq;
using WebApiGateway.Models.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public class CashControllerBaseTestVerifierDepositBuilder : CashControllerBaseTestVerifierBuilder<TransactionModelApi, DepositResponse, Empty>
    {
        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, DepositResponse, Empty>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            var transactionApis = Builder<TransactionApi>
                .CreateListOfSize(3)
                .Build();

            _cashServiceClientRequest = Builder<TransactionModelApi>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .With(x => x.TransactionApis = transactionApis.ToList())
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, DepositResponse, Empty>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            _cashServiceClientResponse = Builder<DepositResponse>
                .CreateNew()
                .Build();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, DepositResponse, Empty>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_cashServiceClientResponse);

            _mockCashServiceClient
                .Setup(f => f.DepositAsync(
                    It.IsAny<DepositRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override CashControllerBaseTestVerifierBuilder<TransactionModelApi, DepositResponse, Empty>
            SetExpectedResult(params string[] paramsStrings)
        {
            return this;
        }
    }
}
