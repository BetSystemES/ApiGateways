using AuthService.Grpc;

using Grpc.Net.ClientFactory;

using Moq;

using ProfileService.GRPC;

using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;

namespace WebApiGateway.UnitTests.Infrastructure.Verifiers
{
    public class BonusControllerTestVerifier<TRequest, TExpectedResult>
        where TRequest : class
        where TExpectedResult : class
    {
        public BonusControllerTestVerifier(
            BonusController bonusController,
            TRequest bonusServiceClientRequest,
            Mock<GrpcClientFactory> mockGrpcClientFactory,
            Mock<ProfileService.GRPC.ProfileService.ProfileServiceClient> mockProfileServiceClient,
            ApiResponse<TExpectedResult> expectedResult)
        {
            BonusController = bonusController;
            RequestModel = bonusServiceClientRequest;
            MockGrpcClientFactory = mockGrpcClientFactory;
            ProfileServiceClient = mockProfileServiceClient;
            ExpectedResult = expectedResult;
        }

        public BonusController BonusController { get; }
        public Mock<GrpcClientFactory> MockGrpcClientFactory { get; }
        public Mock<ProfileService.GRPC.ProfileService.ProfileServiceClient> ProfileServiceClient { get; }
        public TRequest RequestModel { get; }
        public ApiResponse<TExpectedResult> ExpectedResult { get; }

        public BonusControllerTestVerifier<TRequest, TExpectedResult> VerifyGrpcClientFactoryCreateClient()
        {
            MockGrpcClientFactory
                .Verify(f => f.CreateClient<ProfileService.GRPC.ProfileService.ProfileServiceClient>(It.IsAny<string>()), Times.Once());

            return this;
        }

        public BonusControllerTestVerifier<TRequest, TExpectedResult> VerifyProfileServiceClientGetDiscountsAsync()
        {
            ProfileServiceClient
                .Verify(f => f.GetDiscountsAsync(
                    It.IsAny<GetDiscountsRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public BonusControllerTestVerifier<TRequest, TExpectedResult> VerifyProfileServiceClientGetPagedDiscountsAsync()
        {
            ProfileServiceClient
                .Verify(f => f.GetPagedDiscountsAsync(
                    It.IsAny<GetDiscountsWithFilterRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public BonusControllerTestVerifier<TRequest, TExpectedResult> VerifyProfileServiceClientAddDiscountAsync()
        {
            ProfileServiceClient
                .Verify(f => f.AddDiscountAsync(
                    It.IsAny<AddDiscountRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public BonusControllerTestVerifier<TRequest, TExpectedResult> VerifyProfileServiceClientUpdateDiscountAsync()
        {
            ProfileServiceClient
                .Verify(f => f.UpdateDiscountAsync(
                    It.IsAny<UpdateDiscountRequest>(),
                    null, null,
                    It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }
    }
}
