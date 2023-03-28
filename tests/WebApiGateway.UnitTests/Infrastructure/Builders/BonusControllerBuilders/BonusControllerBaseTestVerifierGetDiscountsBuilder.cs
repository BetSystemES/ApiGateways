using FizzWare.NBuilder;
using Moq;
using ProfileService.GRPC;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.ProfileService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders
{
    public class BonusControllerBaseTestVerifierGetDiscountsBuilder : BonusControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetDiscountsResponse, List<DiscountModel>>
    {
        public override BonusControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetDiscountsResponse, List<DiscountModel>>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _bonusServiceClientRequest = Builder<BaseProfileRequstModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetDiscountsResponse, List<DiscountModel>>
            SetProfileServiceClientResponse(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);
            string? userId = paramsStrings.ElementAtOrDefault(1);

            var discounts = Builder<Discount>
                .CreateListOfSize(3)
                .All()
                .With(x => x.Id = id ?? Guid.NewGuid().ToString())
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            _bonusServiceClientResponse = Builder<GetDiscountsResponse>
                .CreateNew()
                .Build();

            _bonusServiceClientResponse.Discounts.AddRange(discounts);

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetDiscountsResponse, List<DiscountModel>>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_bonusServiceClientResponse);

            _mockProfileServiceClient
                .Setup(f => f.GetDiscountsAsync(
                    It.IsAny<GetDiscountsRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BaseProfileRequstModel, GetDiscountsResponse, List<DiscountModel>>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);

            var discounts = Builder<DiscountModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.Id = id ?? Guid.NewGuid().ToString())
                .Build();

            _expectedResult = Builder<ApiResponse<List<DiscountModel>>>
                .CreateNew()
                .With(x => x.Data = discounts.ToList())
                .Build();

            return this;
        }
    }
}
