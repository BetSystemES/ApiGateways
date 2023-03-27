using FizzWare.NBuilder;
using Google.Protobuf.WellKnownTypes;
using Moq;
using ProfileService.GRPC;
using WebApiGateway.Models.ProfileService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders
{
    public class BonusControllerBaseTestVerifierPutBuilder : BonusControllerBaseTestVerifierBuilder<DiscountModel, UpdateDiscountResponse, Empty>
    {
        public override BonusControllerBaseTestVerifierBuilder<DiscountModel, UpdateDiscountResponse, Empty>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _bonusServiceClientRequest = Builder<DiscountModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<DiscountModel, UpdateDiscountResponse, Empty>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            _bonusServiceClientResponse = Builder<UpdateDiscountResponse>
                .CreateNew()
                .Build();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<DiscountModel, UpdateDiscountResponse, Empty>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_bonusServiceClientResponse);

            _mockProfileServiceClient
                .Setup(f => f.UpdateDiscountAsync(
                    It.IsAny<UpdateDiscountRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<DiscountModel, UpdateDiscountResponse, Empty> SetExpectedResult(params string[] paramsStrings)
        {
            return this;
        }
    }
}
