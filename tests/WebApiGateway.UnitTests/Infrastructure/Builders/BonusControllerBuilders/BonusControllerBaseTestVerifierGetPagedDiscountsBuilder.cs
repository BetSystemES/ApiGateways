﻿using FizzWare.NBuilder;
using Moq;
using ProfileService.GRPC;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.BonusService;
using WebApiGateway.Models.ProfileService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders
{
    public class BonusControllerBaseTestVerifierGetPagedDiscountsBuilder : BonusControllerBaseTestVerifierBuilder<BonusServiceRequestModel, GetPagedDiscountsResponse, BasePagedResponseModel<DiscountModel>>
    {
        public override BonusControllerBaseTestVerifierBuilder<BonusServiceRequestModel, GetPagedDiscountsResponse, BasePagedResponseModel<DiscountModel>>
            SetProfileServiceClientRequest(params string[] paramsStrings)
        {
            string? userId = paramsStrings.ElementAtOrDefault(0);

            _bonusServiceClientRequest = Builder<BonusServiceRequestModel>
                .CreateNew()
                .With(x => x.ProfileId = userId ?? Guid.NewGuid().ToString())
                .Build();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BonusServiceRequestModel, GetPagedDiscountsResponse, BasePagedResponseModel<DiscountModel>>
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

            _bonusServiceClientResponse = Builder<GetPagedDiscountsResponse>
                .CreateNew()
                .Build();

            _bonusServiceClientResponse.Discounts.AddRange(discounts);

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BonusServiceRequestModel, GetPagedDiscountsResponse, BasePagedResponseModel<DiscountModel>>
            SetupProfileServiceClientResponse()
        {
            var grpcResponse = GrpcAsyncUnaryCallBuilder(_bonusServiceClientResponse);

            _mockProfileServiceClient
                .Setup(f => f.GetPagedDiscountsAsync(
                    It.IsAny<GetDiscountsWithFilterRequest>(),
                    null,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(grpcResponse)
                .Verifiable();

            return this;
        }

        public override BonusControllerBaseTestVerifierBuilder<BonusServiceRequestModel, GetPagedDiscountsResponse, BasePagedResponseModel<DiscountModel>>
            SetExpectedResult(params string[] paramsStrings)
        {
            string? id = paramsStrings.ElementAtOrDefault(0);

            var discounts = Builder<DiscountModel>
                .CreateListOfSize(3)
                .All()
                .With(x => x.Id = id ?? Guid.NewGuid().ToString())
                .Build();

            _expectedResult = Builder<ApiResponse<BasePagedResponseModel<DiscountModel>>>
                .CreateNew()
                .With(x => x.Data = Builder<BasePagedResponseModel<DiscountModel>>
                    .CreateNew()
                    .With(y => y.Data = discounts.ToList())
                    .Build())
                .Build();

            return this;
        }
    }
}
